using DXP.SmartConnectPickup.BusinessServices.Interfaces;
using DXP.SmartConnectPickup.BusinessServices.Models;
using DXP.SmartConnectPickup.BusinessServices.PickupProcessing;
using DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters;
using DXP.SmartConnectPickup.Common.ApplicationSettings;
using DXP.SmartConnectPickup.Common.Constants;
using DXP.SmartConnectPickup.Common.Enums;
using DXP.SmartConnectPickup.Common.Exceptions;
using DXP.SmartConnectPickup.Common.Models;
using DXP.SmartConnectPickup.Common.Services;
using DXP.SmartConnectPickup.Common.Utils;
using DXP.SmartConnectPickup.DataServices.Interfaces;
using DXP.SmartConnectPickup.DataServices.Models;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DXP.SmartConnectPickup.BusinessServices.Services
{
    public class OrderService : GenericService, IOrderService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationSettings _applicationSettings;
        private readonly MerchantAccountSettings _merchantAccountSettings;
        private readonly ITransactionLogService _transactionLogService;
        private readonly IOrderRepository _orderRepository;
        private readonly ISiteRepository _siteRepository;
        private readonly IPickupAdapterFactory _pickupAdapterFactory;
        private readonly IService_Service _service_Service;

        public OrderService(
            IMapper mapper,
            IOptions<ApplicationSettings> applicationSettings,
            IOptions<MerchantAccountSettings> merchantAccountSettings,
            ITransactionLogService transactionLogService,
            IOrderRepository orderRepository,
            ISiteRepository siteRepository,
            IPickupAdapterFactory pickupAdapterFactory,
            IService_Service service_Service)
        {
            _mapper = mapper;
            _applicationSettings = applicationSettings.Value;
            _merchantAccountSettings = merchantAccountSettings.Value;
            _transactionLogService = transactionLogService;
            _orderRepository = orderRepository;
            _siteRepository = siteRepository;
            _pickupAdapterFactory = pickupAdapterFactory;
            _service_Service = service_Service;
        }

        /// <summary>
        /// Gets Order By User Id.
        /// </summary>
        /// <param name="orderApiId">The orderApiId.</param>
        /// <param name="isViaMerchant">The isViaMerchant.</param>
        /// <returns>Task{BaseResponseObject}.</returns>
        public async Task<BaseResponseObject> GetOrderByOfferApiId(string orderApiId, bool isViaMerchant = false)
        {
            Order order = await _orderRepository.GetOrderByOrderApiIdAsync(orderApiId);
            OrderViewModel orderResponse = new OrderViewModel();
            if (order != null && !string.IsNullOrEmpty(order.ExternalId) && isViaMerchant)
            {
                // Builds pickup adapter
                IPickupTarget pickupTarget = PickupHelper.BuildPickupAdapter(_pickupAdapterFactory, _merchantAccountSettings.PickupProviderDefault);
                BaseRequestObject request = _mapper.Map<GetOrderRequest>(order);
                BaseOrderResponse response = await pickupTarget.GetOrderAsync((GetOrderRequest)request, request.GetCorrelationId());

                await PickupHelper.HandleErrorResponse(_transactionLogService, request, response, TransactionLogStep.GetOrder);

                // Set Sync = null when overwrite
                if (!string.IsNullOrEmpty(response.ExternalId))
                {
                    order.IsSync = null;
                    order.ExternalId = response.ExternalId;
                    order.ExternalStatus = response.OrderState;
                    order.RedemptionCode = response.RedemptionCode;
                    order.RedemptionUrl = response.RedemptionUrl;

                    orderResponse = _mapper.Map<BaseOrderResponse, OrderViewModel>(response);
                }
            }

            orderResponse = _mapper.Map(order, orderResponse);

            return ReturnSuccess(orderResponse);
        }


        /// <summary>
        /// Creates a Order Async.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task{BaseResponseObject}.</returns>
        public async Task<BaseResponseObject> CreateOrderAsync(OrderModel model)
        {
            Guard.AgainstNullOrEmpty(nameof(model.OrderApiId), model.OrderApiId);
            Guard.AgainstNullOrEmpty(nameof(model.OrderNumber), model.OrderNumber);
            Guard.AgainstNullOrEmpty(nameof(model.StoreId), model.StoreId);
            Guard.AgainstNullOrEmpty(nameof(model.ServiceId), model.ServiceId);
            Guard.AgainstNullOrEmpty(nameof(model.OrderStatus), model.OrderStatus);

            Guard.AgainstInvalidArgumentWithMessage($"{nameof(model.OrderStatus)} is Invalid!", Enum.IsDefined(typeof(OrderStatus), model.OrderStatus));

            if (model.PickupType != null)
                Guard.AgainstInvalidArgumentWithMessage($"{nameof(model.PickupType)} is Invalid!", Enum.IsDefined(typeof(PickupType), model.PickupType));

            model.PickupType ??= _merchantAccountSettings.PickupTypeDefault;

            Order existOrder = await _orderRepository.GetOrderByOrderApiIdAsync(model.OrderApiId);
            Guard.AgainstInvalidArgumentWithMessage($"Order is already created.", existOrder == null);

            Site site = await _siteRepository.GetSiteByStoreIdAndProvider(model.StoreId, _merchantAccountSettings.PickupProviderDefault);
            Guard.AgainstInvalidArgumentWithMessage($"Site is not found!.", site != null);

            Service service =  await _service_Service.GetServicesByIdAsync(model.ServiceId);

            Order order = model.Adapt<Order>();

            order.Id = Guid.NewGuid().ToString();
            order.StoreId = model.StoreId;
            order.ExternalSiteId = site.ExternalId;
            order.OrderApiId = model.OrderApiId;
            order.StoreService = model.ServiceId;
            order.DisplayId = service != null ? service.ServiceShortName +"_" + model.OrderNumber : model.OrderNumber;
            order.Provider = _merchantAccountSettings.PickupProviderDefault;
            order.IsSync = false;
            order.ExternalId = null;

            order.CreatedBy = AuthorizationConstants.SITE_ADMIN_ROLE;
            order.CreatedDate = DateTime.UtcNow;
            order.ModifiedBy = AuthorizationConstants.SITE_ADMIN_ROLE;
            order.ModifiedDate = DateTime.UtcNow;

            await _orderRepository.AddAndSaveChangesAsync(order);

            BaseOrderResponse response = await UpdateOrderMerchantAsync(order);

            OrderViewModel orderResponse = _mapper.Map<BaseOrderResponse, OrderViewModel>(response);
            orderResponse = _mapper.Map(order, orderResponse);

            return ReturnSuccess(orderResponse);
        }

        /// <summary>
        /// Update a Order Async.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task{BaseResponseObject}.</returns>
        public async Task<BaseResponseObject> UpdateOrderAsync(OrderModel model)
        {
            Order order = await _orderRepository.GetOrderByOrderApiIdAsync(model.OrderApiId);

            Guard.AgainstNotFound(nameof(model.OrderApiId), order);

            order.OrderStatus = model.OrderStatus ?? order.OrderStatus;
            order.PickupWindow = model.PickupWindow;
            order.PickupType = model.PickupType ?? _merchantAccountSettings.PickupTypeDefault;

            order.ModifiedBy = AuthorizationConstants.SITE_ADMIN_ROLE;
            order.ModifiedDate = DateTime.UtcNow;
            order.IsSync = false;

            await _orderRepository.UpdateAndSaveChangesAsync(order);

            BaseOrderResponse response =  await UpdateOrderMerchantAsync(order);

            OrderViewModel orderResponse = _mapper.Map<BaseOrderResponse, OrderViewModel>(response);
            orderResponse = _mapper.Map(order, orderResponse);

            return ReturnSuccess(orderResponse);
        }

        /// <summary>
        /// Change State Order Event.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task{BaseResponseObject}.</returns>
        public async Task<BaseResponseObject> ChangeStateOrderEvent(OrderModel model)
        {
            Guard.AgainstNullOrEmpty(nameof(model.OrderApiId), model.OrderApiId);
            Guard.AgainstNullOrEmpty(nameof(model.OrderStatus), model.OrderStatus);

            Guard.AgainstInvalidArgumentWithMessage($"{nameof(model.OrderStatus)} is Invalid!", Enum.IsDefined(typeof(OrderStatus), model.OrderStatus));
            Guard.AgainstInvalidArgumentWithMessage(nameof(model.OrderStatus), model.OrderStatus != OrderStatus.New.ToString());

            Order order = await _orderRepository.GetOrderByOrderApiIdAsync(model.OrderApiId);

            Guard.AgainstNotFound(nameof(model.OrderApiId), order);

            if(order.ExternalId == null)
            {
                BaseOrderResponse responseUpdateMerchant = await UpdateOrderMerchantAsync(order);
                string errorMessage = responseUpdateMerchant.Error != null ? JsonConvert.SerializeObject(responseUpdateMerchant.Error) : null;
                errorMessage ??= responseUpdateMerchant.Errors != null ? JsonConvert.SerializeObject(responseUpdateMerchant.Errors) : responseUpdateMerchant.RequestError?.Message;

                if (order.ExternalId == null)
                {
                    return new BaseResponseObject()
                    {
                        Status = false,
                        ErrorCode = responseUpdateMerchant.RequestError != null ? ResponseErrorCode.SystemError : ResponseErrorCode.UnhandleException,
                        Message = errorMessage,
                        StackTrace = !_applicationSettings.IsProduction ? responseUpdateMerchant.RequestError?.StackTrace : string.Empty
                    };
                }
            }

            // Builds pickup adapter
            IPickupTarget pickupTarget = PickupHelper.BuildPickupAdapter(_pickupAdapterFactory, _merchantAccountSettings.PickupProviderDefault);

            var request = _mapper.Map<ChangeStateOrderRequest>(order);

            request.State = GetStateByOrderStatus(model.OrderStatus, _merchantAccountSettings.PickupProviderDefault);

            ChangeStateOrderResponse response = await pickupTarget.ChangeStateOrder(request, request.GetCorrelationId());

            await PickupHelper.HandleErrorResponse(_transactionLogService, request, response, TransactionLogStep.GetOrder);

            if(response.IsSucess)
            {
                order.ExternalStatus = request.State;
                order.OrderStatus = model.OrderStatus;

                await _orderRepository.UpdateAndSaveChangesAsync(order);

                return ReturnSuccess(null, "State order had been update success!");
            }
            else
            {
                string errorMessage = response.Error != null ? JsonConvert.SerializeObject(response.Error) : null;

                errorMessage ??= response.Errors != null ? JsonConvert.SerializeObject(response.Errors) : response.RequestError?.Message;

                return new BaseResponseObject()
                {
                    Status = false,
                    ErrorCode = response.RequestError != null ? ResponseErrorCode.SystemError : ResponseErrorCode.UnhandleException,
                    Message = errorMessage,
                    StackTrace = !_applicationSettings.IsProduction ? response.RequestError?.StackTrace : string.Empty
                };
            }
        }

        /// <summary>
        /// Retry Update a Order Merchant Async.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="orderApiId">The orderApiId.</param>
        /// <returns>Task{BaseResponseObject}.</returns>
        public async Task<BaseResponseObject> RetryUpdateOrderMerchantAsync(string token, string orderApiId)
        {
            Guard.AgainstInvalidArgument(nameof(token), token == _merchantAccountSettings.TokenRetry);

            Order order = await _orderRepository.GetOrderByOrderApiIdAsync(orderApiId);

            Guard.AgainstNotFound(nameof(orderApiId), order);

            BaseOrderResponse response = await UpdateOrderMerchantAsync(order);

            if (!string.IsNullOrEmpty(response.ExternalId))
            {
                OrderViewModel orderResponse = _mapper.Map<BaseOrderResponse, OrderViewModel>(response);
                orderResponse = _mapper.Map(order, orderResponse);

                return ReturnSuccess(orderResponse);
            }
            else
            {
                return new BaseResponseObject()
                {
                    Status = false,
                    ErrorCode = response.RequestError != null ? ResponseErrorCode.SystemError : ResponseErrorCode.UnhandleException,
                    Message = response.Errors != null ? JsonConvert.SerializeObject(response.Errors) : response.RequestError?.Message,
                    StackTrace = !_applicationSettings.IsProduction ? response.RequestError?.StackTrace : string.Empty
                };
            }
        }

        /// <summary>
        /// Retry Update Mutil Order Merchant Async.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="length">The length.</param>
        /// <param name="skipIndex">The skipIndex.</param>
        /// <returns>Task{BaseResponseObject}.</returns>
        public async Task<BaseResponseObject> RetryUpdateMutilOrderMerchantAsync(string token, int length = 10, int skipIndex = 1)
        {
            Guard.AgainstInvalidArgument(nameof(token), token == _merchantAccountSettings.TokenRetry);

            length = length > _merchantAccountSettings.MaxNumberOrderRetry ? _merchantAccountSettings.MaxNumberOrderRetry : length;

            IEnumerable<Order> Orders = await _orderRepository
                .GetOrderNotSyncByProviderAsync(_merchantAccountSettings.PickupProviderDefault, length, skipIndex);

            int totalSuccess = 0;
            foreach (var Order in Orders)
            {
                BaseOrderResponse response = await UpdateOrderMerchantAsync(Order);

                if (!string.IsNullOrEmpty(response.ExternalId))
                {
                    totalSuccess++;
                }
            }

            return ReturnSuccess(null, $"Update Order Merchant Sync Success {totalSuccess} of {length}");
        }

        private async Task<BaseOrderResponse> UpdateOrderMerchantAsync(Order order)
        {
            // Builds pickup adapter
            IPickupTarget pickupTarget = PickupHelper.BuildPickupAdapter(_pickupAdapterFactory, _merchantAccountSettings.PickupProviderDefault);

            TransactionLogStep transactionLogStep;
            BaseOrderResponse response;
            BaseRequestObject request;

            // Case: add to flybuy not success
            if (string.IsNullOrEmpty(order.ExternalId))
            {
                transactionLogStep = TransactionLogStep.CreateOrder;
                var _request = _mapper.Map<CreateOrderRequest>(order);
                _request.State = GetStateByOrderStatus(order.OrderStatus, _merchantAccountSettings.PickupProviderDefault);
                _request.PickupType = GetPickupTypeForMerchantRequest(order.PickupType, _merchantAccountSettings.PickupProviderDefault);

                response = await pickupTarget.CreateOrderAsync(_request, _request.GetCorrelationId());
                request = _request;
            }
            else
            {
                transactionLogStep = TransactionLogStep.UpdateOrder;
                var _request = _mapper.Map<UpdateOrderRequest>(order);
                _request.State = GetStateByOrderStatus(order.OrderStatus, _merchantAccountSettings.PickupProviderDefault);
                _request.PickupType = GetPickupTypeForMerchantRequest(order.PickupType, _merchantAccountSettings.PickupProviderDefault);

                response = await pickupTarget.UpdateOrderAsync(_request, _request.GetCorrelationId());
                request = _request;
            }

            await UpdateExternalId(order, response);
            await PickupHelper.HandleErrorResponse(_transactionLogService, request, response, transactionLogStep);

            return response;
        }

        private static string GetStateByOrderStatus(string orderStatus, string provider)
        {
            var state = PickupState.Created;

            if (orderStatus == OrderStatus.Cancelled.ToString())
            {
                state = PickupState.Cancelled;
            }
            else if (orderStatus == OrderStatus.Ready.ToString())
            {
                state = PickupState.Ready;
            }
            else if (orderStatus == OrderStatus.Completed.ToString())
            {
                state = PickupState.Completed;
            }

            if (provider == MerchantAccountType.FlyBuy.ToString())
            {
                return ConvertStateForFlyBuyRequest(state);
            }

            return state.ToString();
        }

        private static string ConvertStateForFlyBuyRequest(PickupState state)
        {
            if(state == PickupState.Created)
            {
                return "created";
            }

            if (state == PickupState.Completed)
            {
                return "completed";
            }
            else if (state == PickupState.Cancelled)
            {
                return "cancelled";
            }
            else if (state == PickupState.Ready)
            {
                return "ready";
            }
            else if (state == PickupState.Delayed)
            {
                return "delayed";
            }

            throw new NotFoundException($"The {state} is not support");
        }

        private static string GetPickupTypeForMerchantRequest(string pickupType, string provider)
        {
            if (provider == MerchantAccountType.FlyBuy.ToString())
            {
                return ConvertPickupTypeForFlyBuyRequest(pickupType);
            }

            return pickupType;
        }

        private static string ConvertPickupTypeForFlyBuyRequest(string pickupType)
        {
            if (pickupType == PickupType.Curbside.ToString())
            {
                return "curbside";
            }
            else if (pickupType == PickupType.Delivery.ToString())
            {
                return "delivery";
            }
            else if (pickupType == PickupType.Dispatch.ToString())
            {
                return "dispatch";
            }
            else if (pickupType == PickupType.DriveThru.ToString())
            {
                return "drive_thru";
            }
            else if (pickupType == PickupType.Pickup.ToString())
            {
                return "pickup";
            }

            return pickupType.ToString();
        }

        private async Task UpdateExternalId<T>(Order order, T response) where T : BaseOrderResponse
        {
            if (!string.IsNullOrEmpty(response.ExternalId))
            {
                order.ExternalId = response.ExternalId;
                order.ExternalStatus = response.OrderState.ToString();
                order.RedemptionCode = response.RedemptionCode;
                order.RedemptionUrl = response.RedemptionUrl;
                order.IsSync = true;
                order.ModifiedDate = DateTime.Now;
                order.ModifiedBy = AuthorizationConstants.SITE_ADMIN_ROLE;

                await _orderRepository.UpdateAndSaveChangesAsync(order);
            }
        }
    }
}
