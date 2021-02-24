using DXP.SmartConnectPickup.BusinessServices.Interfaces;
using DXP.SmartConnectPickup.BusinessServices.Models;
using DXP.SmartConnectPickup.BusinessServices.PickupProcessing;
using DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters;
using DXP.SmartConnectPickup.Common.ApplicationSettings;
using DXP.SmartConnectPickup.Common.Constants;
using DXP.SmartConnectPickup.Common.Enums;
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
        private readonly IStoreService_Service _storeService_Service;

        public OrderService(
            IMapper mapper,
            IOptions<ApplicationSettings> applicationSettings,
            IOptions<MerchantAccountSettings> merchantAccountSettings,
            ITransactionLogService transactionLogService,
            IOrderRepository orderRepository,
            ISiteRepository siteRepository,
            IPickupAdapterFactory pickupAdapterFactory,
            IStoreService_Service storeService_Service)
        {
            _mapper = mapper;
            _applicationSettings = applicationSettings.Value;
            _merchantAccountSettings = merchantAccountSettings.Value;
            _transactionLogService = transactionLogService;
            _orderRepository = orderRepository;
            _siteRepository = siteRepository;
            _pickupAdapterFactory = pickupAdapterFactory;
            _storeService_Service = storeService_Service;
        }

        /// <summary>
        /// Gets Order By User Id.
        /// </summary>
        /// <param name="orderApiId">The userId.</param>
        /// <param name="isViaMerchant">The isViaMerchant.</param>
        /// <returns>Task{BaseResponseObject}.</returns>
        public async Task<BaseResponseObject> GetOrderByUserId(string orderApiId, bool isViaMerchant = false)
        {
            Order order = await _orderRepository.GetOrderByOrderApiIdAsync(orderApiId);

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
                    order.ExternalStatus = response.State;
                    order.RedemptionCode = response.RedemptionCode;
                    order.RedemptionUrl = response.RedemptionUrl;
                }
            }

            return ReturnSuccess(_mapper.Map<Order, OrderViewModel>(order));
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
            Guard.AgainstNullOrEmpty(nameof(model.StoreServiceId), model.StoreServiceId);
            Guard.AgainstNullOrEmpty(nameof(model.OrderStatus), model.OrderStatus);

            Guard.AgainstInvalidArgumentWithMessage($"{nameof(model.OrderStatus)} is Invalid!", Enum.IsDefined(typeof(OrderStatus), model.OrderStatus));

            if (model.PickupType != null)
                Guard.AgainstInvalidArgumentWithMessage($"{nameof(model.PickupType)} is Invalid!", Enum.IsDefined(typeof(PickupType), model.PickupType));

            model.PickupType ??= _merchantAccountSettings.PickupTypeDefault;

            Order existOrder = await _orderRepository.GetOrderByOrderApiIdAsync(model.OrderApiId);
            Guard.AgainstInvalidArgumentWithMessage($"Order is already created.", existOrder == null);

            Site site = await _siteRepository.GetSiteByStoreIdAndProvider(model.StoreId, _merchantAccountSettings.PickupProviderDefault);
            Guard.AgainstInvalidArgumentWithMessage($"Site is not found!.", site != null);

            StoreService service =  await _storeService_Service.GetStoreServicesById(model.StoreServiceId);

            Order order = model.Adapt<Order>();

            order.Id = Guid.NewGuid().ToString();
            order.StoreId = model.StoreId;
            order.ExternalSiteId = site.ExternalId;
            order.OrderApiId = model.OrderApiId;
            order.StoreService = model.StoreServiceId;
            order.DisplayId = service != null ? service.ServiceShortName +"_" + model.OrderNumber : model.OrderNumber;
            order.Provider = _merchantAccountSettings.PickupProviderDefault;
            order.IsSync = false;
            order.ExternalId = null;

            order.CreatedBy = AuthorizationConstants.SITE_ADMIN_ROLE;
            order.CreatedDate = DateTime.UtcNow;
            order.ModifiedBy = AuthorizationConstants.SITE_ADMIN_ROLE;
            order.ModifiedDate = DateTime.UtcNow;

            await _orderRepository.AddAndSaveChangesAsync(order);

            await UpdateOrderMerchantAsync(order);

            return ReturnSuccess(_mapper.Map<Order, OrderViewModel>(order));
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

            await UpdateOrderMerchantAsync(order);

            return ReturnSuccess(_mapper.Map<Order, OrderViewModel>(order));
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

            return "created";
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
                order.ExternalStatus = response.State.ToString();
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
