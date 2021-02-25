using DXP.SmartConnectPickup.Common.ApplicationSettings;
using DXP.SmartConnectPickup.Common.WebApi;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters.FlyBuy
{
    public class FlyBuyApiAdaptee : WebApiClient, IFlyBuyApiAdaptee
    {
        private readonly MerchantAccountSettings _appSettings;
        private readonly ILogger<FlyBuyApiAdaptee> _logger;

        public FlyBuyApiAdaptee(IOptions<MerchantAccountSettings> options,
            ILogger<FlyBuyApiAdaptee> logger,
            HttpClient httpClient) : base(logger, httpClient)
        {
            _appSettings = options.Value;
            _logger = logger;
        }

        private static class PathConstants
        {
            //Customer
            public const string CreateCustomer = "/customers";
            public const string UpdateCustomer = "/customers/{0}";
            public const string GetCustomer = "/customers/{0}";

            // Order CRUD
            public const string CreateOrder = "/orders";
            public const string UpdateOrder = "/orders/{0}";
            public const string GetOrderById = "/orders/{0}";

            // Order Event
            public const string SendOrderEvent = "/events";
            public const string OrderEventTypeStateChange = "state_change";
        }

        /// <summary>
        /// Create Customer Async.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="correlationId">The correlationId.</param>
        /// <returns>Task{FlyBuyCreateCustomerResponse}.</returns>
        public async Task<FlyBuyCreateCustomerResponse> CreateCustomerAsync(FlyBuyCreateCustomerRequest request, string correlationId)
        {
            // Logs request data
            _logger.LogInformation($"Call to {nameof(CreateCustomerAsync)} with correlationId: {correlationId}, request: {JsonConvert.SerializeObject(request)}.");

            AddRequestHeaders();

            string providerName = _appSettings.FlyBuy.ProviderName;
            string requestUri = $"{_appSettings.FlyBuy.ApiUrl}{PathConstants.CreateCustomer}";
            FluentUriBuilder httpRequest = CreateRequest(requestUri);

            FlyBuyCreateCustomerResponse response = null;
            Exception exception = null;
            try
            {
                response = await PostAsync<FlyBuyCreateCustomerRequest, FlyBuyCreateCustomerResponse>(
                $"Post Create Customer Request {requestUri}",
                httpRequest.Uri,
                request,
                CancellationToken.None,
                DataInterchangeFormat.Json,
                providerName,
                jsonSerializerSettings: null,
                ignoreChecking: true);
            }
            catch (Exception ex)
            {
                exception = ex;
                _logger.LogError(ex, $"Call to {nameof(CreateCustomerAsync)} with correlationId: {correlationId} failed.");
            }

            if (response == null)
            {
                response = new FlyBuyCreateCustomerResponse();
            }

            response.RequestError = exception;
            response.RequestData = request;
            response.RequestUrl = requestUri;

            // Logs response data
            _logger.LogInformation($"Call to {nameof(CreateCustomerAsync)} with correlationId: {correlationId}, response: {JsonConvert.SerializeObject(response)}.");
            return response;
        }

        /// <summary>
        /// Update Customer Async.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="correlationId">The correlationId.</param>
        /// <returns>Task{FlyBuyUpdateCustomerResponse}.</returns>
        public async Task<FlyBuyUpdateCustomerResponse> UpdateCustomerAsync(FlyBuyUpdateCustomerRequest request, string correlationId)
        {
            // Logs request data
            _logger.LogInformation($"Call to {nameof(UpdateCustomerAsync)} with correlationId: {correlationId}, request: {JsonConvert.SerializeObject(request)}.");

            AddRequestHeaders();

            string providerName = _appSettings.FlyBuy.ProviderName;
            string requestUri = string.Format($"{_appSettings.FlyBuy.ApiUrl}{PathConstants.UpdateCustomer}", request.Id);
            FluentUriBuilder httpRequest = CreateRequest(requestUri);

            FlyBuyUpdateCustomerResponse response = null;
            Exception exception = null;
            try
            {
                response = await PutAsync<FlyBuyUpdateCustomerRequest, FlyBuyUpdateCustomerResponse>(
                $"Post Update Customer Request {requestUri}",
                httpRequest.Uri,
                request,
                CancellationToken.None,
                providerName,
                ignoreChecking: true);
            }
            catch (Exception ex)
            {
                exception = ex;
                _logger.LogError(ex, $"Call to {nameof(UpdateCustomerAsync)} with correlationId: {correlationId} failed.");
            }

            if (response == null)
            {
                response = new FlyBuyUpdateCustomerResponse();
            }

            response.RequestError = exception;
            response.RequestData = request;
            response.RequestUrl = requestUri;

            // Logs response data
            _logger.LogInformation($"Call to {nameof(UpdateCustomerAsync)} with correlationId: {correlationId}, response: {JsonConvert.SerializeObject(response)}.");
            return response;
        }

        /// <summary>
        /// Get Customer Async.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="correlationId">The correlationId.</param>
        /// <returns>Task{FlyBuyGetCustomerResponse}.</returns>
        public async Task<FlyBuyGetCustomerResponse> GetCustomerAsync(FlyBuyGetCustomerRequest request, string correlationId)
        {
            // Logs request data
            _logger.LogInformation($"Call to {nameof(GetCustomerAsync)} with correlationId: {correlationId}, request: {JsonConvert.SerializeObject(request)}.");

            AddRequestHeaders();

            string providerName = _appSettings.FlyBuy.ProviderName;
            string requestUri = string.Format($"{_appSettings.FlyBuy.ApiUrl}{PathConstants.GetCustomer}", request.Id);
            FluentUriBuilder httpRequest = CreateRequest(requestUri);

            FlyBuyGetCustomerResponse response = null;
            Exception exception = null;
            try
            {
                response = await GetAsync<FlyBuyGetCustomerResponse>(
                $"Get Customer Request {requestUri}",
                httpRequest.Uri,
                CancellationToken.None,
                providerName,
                ignoreChecking: true);
            }
            catch (Exception ex)
            {
                exception = ex;
                _logger.LogError(ex, $"Call to {nameof(GetCustomerAsync)} with correlationId: {correlationId} failed.");
            }

            if (response == null)
            {
                response = new FlyBuyGetCustomerResponse();
            }

            response.RequestError = exception;
            response.RequestData = request;
            response.RequestUrl = requestUri;

            // Logs response data
            _logger.LogInformation($"Call to {nameof(GetCustomerAsync)} with correlationId: {correlationId}, response: {JsonConvert.SerializeObject(response)}.");
            return response;
        }

        /// <summary>
        /// Create Order Async.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="correlationId">The correlationId.</param>
        /// <returns>Task{FlyBuyCreateOrderResponse}.</returns>
        public async Task<FlyBuyCreateOrderResponse> CreateOrderAsync(FlyBuyCreateOrderRequest request, string correlationId)
        {
            // Logs request data
            _logger.LogInformation($"Call to {nameof(CreateOrderAsync)} with correlationId: {correlationId}, request: {JsonConvert.SerializeObject(request)}.");

            AddRequestHeaders();

            string providerName = _appSettings.FlyBuy.ProviderName;
            string requestUri = $"{_appSettings.FlyBuy.ApiUrl}{PathConstants.CreateOrder}";
            FluentUriBuilder httpRequest = CreateRequest(requestUri);

            FlyBuyCreateOrderResponse response = null;
            Exception exception = null;
            try
            {
                response = await PostAsync<FlyBuyCreateOrderRequest, FlyBuyCreateOrderResponse>(
                $"Post Create Order Request {requestUri}",
                httpRequest.Uri,
                request,
                CancellationToken.None,
                DataInterchangeFormat.Json,
                providerName,
                jsonSerializerSettings: null,
                ignoreChecking: true);
            }
            catch (Exception ex)
            {
                exception = ex;
                _logger.LogError(ex, $"Call to {nameof(CreateOrderAsync)} with correlationId: {correlationId} failed.");
            }

            if (response == null)
            {
                response = new FlyBuyCreateOrderResponse();
            }

            response.RequestError = exception;
            response.RequestData = request;
            response.RequestUrl = requestUri;
           
            // Logs response data
            _logger.LogInformation($"Call to {nameof(CreateOrderAsync)} with correlationId: {correlationId}, response: {JsonConvert.SerializeObject(response)}.");
            return response;
        }

        /// <summary>
        /// Update Order Async.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="correlationId">The correlationId.</param>
        /// <returns>Task{FlyBuyUpdateOrderResponse}.</returns>
        public async Task<FlyBuyUpdateOrderResponse> UpdateOrderAsync(FlyBuyUpdateOrderRequest request, string correlationId)
        {
            // Logs request data
            _logger.LogInformation($"Call to {nameof(UpdateOrderAsync)} with correlationId: {correlationId}, request: {JsonConvert.SerializeObject(request)}.");

            AddRequestHeaders();

            string providerName = _appSettings.FlyBuy.ProviderName;
            string requestUri = string.Format($"{_appSettings.FlyBuy.ApiUrl}{PathConstants.UpdateOrder}", request.Id);
            FluentUriBuilder httpRequest = CreateRequest(requestUri);

            FlyBuyUpdateOrderResponse response = null;
            Exception exception = null;
            try
            {
                response = await PutAsync<FlyBuyUpdateOrderRequest, FlyBuyUpdateOrderResponse>(
                $"Post Update Order Request {requestUri}",
                httpRequest.Uri,
                request,
                CancellationToken.None,
                providerName,
                ignoreChecking: true);
            }
            catch (Exception ex)
            {
                exception = ex;
                _logger.LogError(ex, $"Call to {nameof(UpdateOrderAsync)} with correlationId: {correlationId} failed.");
            }

            if (response == null)
            {
                response = new FlyBuyUpdateOrderResponse();
            }

            response.RequestError = exception;
            response.RequestData = request;
            response.RequestUrl = requestUri;

            // Logs response data
            _logger.LogInformation($"Call to {nameof(UpdateOrderAsync)} with correlationId: {correlationId}, response: {JsonConvert.SerializeObject(response)}.");
            return response;
        }

        /// <summary>
        /// Get Order Async.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="correlationId">The correlationId.</param>
        /// <returns>Task{FlyBuyGetOrderResponse}.</returns>
        public async Task<FlyBuyGetOrderResponse> GetOrderAsync(FlyBuyGetOrderRequest request, string correlationId)
        {
            // Logs request data
            _logger.LogInformation($"Call to {nameof(GetOrderAsync)} with correlationId: {correlationId}, request: {JsonConvert.SerializeObject(request)}.");

            AddRequestHeaders();

            string providerName = _appSettings.FlyBuy.ProviderName;
            string requestUri = string.Format($"{_appSettings.FlyBuy.ApiUrl}{PathConstants.GetOrderById}", request.Id);
            FluentUriBuilder httpRequest = CreateRequest(requestUri);

            FlyBuyGetOrderResponse response = null;
            Exception exception = null;
            try
            {
                response = await GetAsync<FlyBuyGetOrderResponse>(
                $"Get Order Request {requestUri}",
                httpRequest.Uri,
                CancellationToken.None,
                providerName,
                ignoreChecking: true);
            }
            catch (Exception ex)
            {
                exception = ex;
                _logger.LogError(ex, $"Call to {nameof(GetOrderAsync)} with correlationId: {correlationId} failed.");
            }

            if (response == null)
            {
                response = new FlyBuyGetOrderResponse();
            }

            response.RequestError = exception;
            response.RequestData = request;
            response.RequestUrl = requestUri;

            // Logs response data
            _logger.LogInformation($"Call to {nameof(GetOrderAsync)} with correlationId: {correlationId}, response: {JsonConvert.SerializeObject(response)}.");
            return response;
        }

        /// <summary>
        /// Change State Order.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="correlationId">The correlationId.</param>
        /// <returns>Task{FlyBuyCreateOrderResponse}.</returns>
        public async Task<FlyBuyChangeStateOrderResponse> ChangeStateOrder(FlyBuyChangeStateOrderRequest request, string correlationId)
        {
            // Logs request data
            request.Data.EventType = PathConstants.OrderEventTypeStateChange;
            _logger.LogInformation($"Call to {nameof(ChangeStateOrder)} with correlationId: {correlationId}, request: {JsonConvert.SerializeObject(request)}.");

            AddRequestHeaders();
            string providerName = _appSettings.FlyBuy.ProviderName;
            string requestUri = $"{_appSettings.FlyBuy.ApiUrl}{PathConstants.SendOrderEvent}";
            FluentUriBuilder httpRequest = CreateRequest(requestUri);

            FlyBuyChangeStateOrderResponse response = null;
            Exception exception = null;
            try
            {
                response = await PostAsync<FlyBuyChangeStateOrderRequest, FlyBuyChangeStateOrderResponse>(
                $"Post Change State Order Request {requestUri}",
                httpRequest.Uri,
                request,
                CancellationToken.None,
                DataInterchangeFormat.Json,
                providerName,
                jsonSerializerSettings: null,
                ignoreChecking: true);
            }
            catch (Exception ex)
            {
                exception = ex;
                _logger.LogError(ex, $"Call to {nameof(ChangeStateOrder)} with correlationId: {correlationId} failed.");
            }

            if (response == null)
            {
                response = new FlyBuyChangeStateOrderResponse();
            }

            response.RequestError = exception;
            response.RequestData = request;
            response.RequestUrl = requestUri;

            // Logs response data
            _logger.LogInformation($"Call to {nameof(ChangeStateOrder)} with correlationId: {correlationId}, response: {JsonConvert.SerializeObject(response)}.");
            return response;
        }

        /// <summary>
        /// Change State Order.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="correlationId">The correlationId.</param>
        /// <returns>Task{FlyBuyCreateOrderResponse}.</returns>
        public async Task<FlyBuyCustomerRatingOrderResponse> CustomerRatingOrder(FlyBuyCustomerRatingOrderRequest request, string correlationId)
        {
            // Logs request data
            _logger.LogInformation($"Call to {nameof(CustomerRatingOrder)} with correlationId: {correlationId}, request: {JsonConvert.SerializeObject(request)}.");

            AddRequestHeaders();

            string providerName = _appSettings.FlyBuy.ProviderName;
            string requestUri = $"{_appSettings.FlyBuy.ApiUrl}{PathConstants.SendOrderEvent}";
            FluentUriBuilder httpRequest = CreateRequest(requestUri);

            FlyBuyCustomerRatingOrderResponse response = null;
            Exception exception = null;
            try
            {
                response = await PostAsync<FlyBuyCustomerRatingOrderRequest, FlyBuyCustomerRatingOrderResponse>(
                $"Post Change State Order Request {requestUri}",
                httpRequest.Uri,
                request,
                CancellationToken.None,
                DataInterchangeFormat.Json,
                providerName,
                jsonSerializerSettings: null,
                ignoreChecking: true);
            }
            catch (Exception ex)
            {
                exception = ex;
                _logger.LogError(ex, $"Call to {nameof(CustomerRatingOrder)} with correlationId: {correlationId} failed.");
            }

            if (response == null)
            {
                response = new FlyBuyCustomerRatingOrderResponse();
            }

            response.RequestError = exception;
            response.RequestData = request;
            response.RequestUrl = requestUri;

            // Logs response data
            _logger.LogInformation($"Call to {nameof(CustomerRatingOrder)} with correlationId: {correlationId}, response: {JsonConvert.SerializeObject(response)}.");
            return response;
        }

        private void AddRequestHeaders()
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Remove("Authorization");

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Token token=\"{_appSettings.FlyBuy.TokenApiSecret}\"");
        }
    }
}
