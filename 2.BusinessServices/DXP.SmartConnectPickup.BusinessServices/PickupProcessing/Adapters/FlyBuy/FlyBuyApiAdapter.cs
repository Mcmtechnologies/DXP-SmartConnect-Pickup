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

        private static void MappingBaseResponse(FlyBuyResponseBase flyBuyResponse, BaseCustomerResponse customer)
        {
            customer.Error = flyBuyResponse.Error;
            customer.RequestData = flyBuyResponse.RequestData;
            customer.RequestError = flyBuyResponse.RequestError;
            customer.RequestHeaders = flyBuyResponse.RequestHeaders;
            customer.RequestUrl = flyBuyResponse.RequestUrl;
            customer.Response = flyBuyResponse.Response;
        }
    }
}
