using MapsterMapper;
using System;
using System.Threading.Tasks;

namespace DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters.FlyBuy
{
    public class FlyBuyApiAdapter : IFlyBuyApiAdapter
    {
        private readonly IFlyBuyApiAdaptee _flyBuyApiAdaptee;
        private readonly IMapper _mapper;

        public FlyBuyApiAdapter(IMapper mapper
            , IFlyBuyApiAdaptee flyBuyApiAdaptee)
        {
            _mapper = mapper;
            _flyBuyApiAdaptee = flyBuyApiAdaptee;
        }

        /// <summary>
        /// Gets Customer Async.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="correlationId">The correlationId.</param>
        /// <returns>Task{GetCustomerResponse}.</returns>
        public async Task<GetCustomerResponse> GetCustomerAsync(GetCustomerRequest request, Guid correlationId)
        {
            return await GetCustomerAsync(request, correlationId.ToString());
        }

        /// <summary>
        /// Gets Customer Async.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="correlationId">The correlationId.</param>
        /// <returns>Task{GetCustomerResponse}.</returns>
        public async Task<GetCustomerResponse> GetCustomerAsync(GetCustomerRequest request, string correlationId)
        {
            var flyBuyRequest = new FlyBuyGetCustomerRequest()
            {
                Id = request.ExternalId
            };

            FlyBuyGetCustomerResponse flyBuyResponse = await _flyBuyApiAdaptee.GetCustomerAsync(flyBuyRequest, correlationId);

            var customer = _mapper.Map<GetCustomerResponse>(flyBuyResponse.Data);
            if (customer == null)
            {
                customer = new GetCustomerResponse();
            }

            MappingBaseResponse(flyBuyResponse, customer);

            return customer;
        }

        /// <summary>
        /// Creates Customer Async.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="correlationId">The correlationId.</param>
        /// <returns>Task{CreateCustomerResponse}.</returns>
        public async Task<CreateCustomerResponse> CreateCustomerAsync(CreateCustomerRequest request, Guid correlationId)
        {
            return await CreateCustomerAsync(request, correlationId.ToString());
        }

        /// <summary>
        /// Creates Customer Async.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="correlationId">The correlationId.</param>
        /// <returns>Task{CreateCustomerResponse}.</returns>
        public async Task<CreateCustomerResponse> CreateCustomerAsync(CreateCustomerRequest request, string correlationId)
        {
            var flyBuyRequest = new FlyBuyCreateCustomerRequest()
            {
                Data = _mapper.Map<FlyBuyCustomerRequestData>(request)
            };

            FlyBuyCreateCustomerResponse flyBuyResponse = await _flyBuyApiAdaptee.CreateCustomerAsync(flyBuyRequest, correlationId);

            var customer = _mapper.Map<CreateCustomerResponse>(flyBuyResponse.Data);
            if (customer == null)
            {
                customer = new CreateCustomerResponse();
            }

            MappingBaseResponse(flyBuyResponse, customer);

            return customer;
        }

        /// <summary>
        /// Update Customer Async.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="correlationId">The correlationId.</param>
        /// <returns>Task{UpdateCustomerResponse}.</returns>
        public async Task<UpdateCustomerResponse> UpdateCustomerAsync(UpdateCustomerRequest request, Guid correlationId)
        {
            return await UpdateCustomerAsync(request, correlationId.ToString());
        }

        /// <summary>
        /// Update Customer Async.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="correlationId">The correlationId.</param>
        /// <returns>Task{UpdateCustomerResponse}.</returns>
        public async Task<UpdateCustomerResponse> UpdateCustomerAsync(UpdateCustomerRequest request, string correlationId)
        {

            var flyBuyRequest = new FlyBuyUpdateCustomerRequest()
            {
                Id = request.ExternalId,
                Data = _mapper.Map<FlyBuyCustomerRequestData>(request)
            };

            FlyBuyUpdateCustomerResponse flyBuyResponse = await _flyBuyApiAdaptee.UpdateCustomerAsync(flyBuyRequest, correlationId);

            var customer = _mapper.Map<UpdateCustomerResponse>(flyBuyResponse.Data);

            if (customer == null)
            {
                customer = new UpdateCustomerResponse();
            }

            MappingBaseResponse(flyBuyResponse, customer);

            return customer;
        }

        /// <summary>
        /// Gets Order Async.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="correlationId">The correlationId.</param>
        /// <returns>Task{GetOrderResponse}.</returns>
        public async Task<GetOrderResponse> GetOrderAsync(GetOrderRequest request, Guid correlationId)
        {
            return await GetOrderAsync(request, correlationId.ToString());
        }

        /// <summary>
        /// Gets Order Async.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="correlationId">The correlationId.</param>
        /// <returns>Task{GetOrderResponse}.</returns>
        public async Task<GetOrderResponse> GetOrderAsync(GetOrderRequest request, string correlationId)
        {
            var flyBuyRequest = new FlyBuyGetOrderRequest()
            {
                Id = request.ExternalId
            };

            FlyBuyGetOrderResponse flyBuyResponse = await _flyBuyApiAdaptee.GetOrderAsync(flyBuyRequest, correlationId);

            var Order = _mapper.Map<GetOrderResponse>(flyBuyResponse.Data);
            if (Order == null)
            {
                Order = new GetOrderResponse();
            }

            MappingBaseResponse(flyBuyResponse, Order);

            return Order;
        }

        /// <summary>
        /// Creates Order Async.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="correlationId">The correlationId.</param>
        /// <returns>Task{CreateOrderResponse}.</returns>
        public async Task<CreateOrderResponse> CreateOrderAsync(CreateOrderRequest request, Guid correlationId)
        {
            return await CreateOrderAsync(request, correlationId.ToString());
        }

        /// <summary>
        /// Creates Order Async.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="correlationId">The correlationId.</param>
        /// <returns>Task{CreateOrderResponse}.</returns>
        public async Task<CreateOrderResponse> CreateOrderAsync(CreateOrderRequest request, string correlationId)
        {
            var flyBuyRequest = new FlyBuyCreateOrderRequest()
            {
                Data = _mapper.Map<FlyBuyOrderRequestData>(request)
            };

            FlyBuyCreateOrderResponse flyBuyResponse = await _flyBuyApiAdaptee.CreateOrderAsync(flyBuyRequest, correlationId);

            var Order = _mapper.Map<CreateOrderResponse>(flyBuyResponse.Data);
            if (Order == null)
            {
                Order = new CreateOrderResponse();
            }

            MappingBaseResponse(flyBuyResponse, Order);

            return Order;
        }

        /// <summary>
        /// Update Order Async.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="correlationId">The correlationId.</param>
        /// <returns>Task{UpdateOrderResponse}.</returns>
        public async Task<UpdateOrderResponse> UpdateOrderAsync(UpdateOrderRequest request, Guid correlationId)
        {
            return await UpdateOrderAsync(request, correlationId.ToString());
        }

        /// <summary>
        /// Update Order Async.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="correlationId">The correlationId.</param>
        /// <returns>Task{UpdateOrderResponse}.</returns>
        public async Task<UpdateOrderResponse> UpdateOrderAsync(UpdateOrderRequest request, string correlationId)
        {

            var flyBuyRequest = new FlyBuyUpdateOrderRequest()
            {
                Id = request.ExternalId,
                Data = _mapper.Map<FlyBuyOrderRequestData>(request)
            };

            FlyBuyUpdateOrderResponse flyBuyResponse = await _flyBuyApiAdaptee.UpdateOrderAsync(flyBuyRequest, correlationId);

            var Order = _mapper.Map<UpdateOrderResponse>(flyBuyResponse.Data);

            if (Order == null)
            {
                Order = new UpdateOrderResponse();
            }

            MappingBaseResponse(flyBuyResponse, Order);

            return Order;
        }

        public async Task<ChangeStateOrderResponse> ChangeStateOrder(ChangeStateOrderRequest request, Guid correlationId)
        {
            return await ChangeStateOrder(request, correlationId.ToString());
        }

        public async Task<ChangeStateOrderResponse> ChangeStateOrder(ChangeStateOrderRequest request, string correlationId)
        {
            var flyBuyRequest = new FlyBuyChangeStateOrderRequest()
            {
                Data = _mapper.Map<FlyBuyOrderEventStateChangeRequestData>(request)
            };

            FlyBuyChangeStateOrderResponse flyBuyResponse = await _flyBuyApiAdaptee.ChangeStateOrder(flyBuyRequest, correlationId);

            var response = new ChangeStateOrderResponse()
            {
                IsSucess = flyBuyResponse.Errors == null && flyBuyResponse.Error == null 
                    && (flyBuyResponse.Response == null || flyBuyResponse.Response.IsSuccessStatusCode)
            };

            MappingBaseResponse(flyBuyResponse, response);

            return response;
        }

        private static void MappingBaseResponse(FlyBuyResponseBase flyBuyResponse, BasePickupResponse pickupResponse)
        {
            pickupResponse.Errors = flyBuyResponse.Errors;
            pickupResponse.Error = flyBuyResponse.Error;
            pickupResponse.RequestData = flyBuyResponse.RequestData;
            pickupResponse.RequestError = flyBuyResponse.RequestError;
            pickupResponse.RequestHeaders = flyBuyResponse.RequestHeaders;
            pickupResponse.RequestUrl = flyBuyResponse.RequestUrl;
            pickupResponse.Response = flyBuyResponse.Response;
        }

    }
}
