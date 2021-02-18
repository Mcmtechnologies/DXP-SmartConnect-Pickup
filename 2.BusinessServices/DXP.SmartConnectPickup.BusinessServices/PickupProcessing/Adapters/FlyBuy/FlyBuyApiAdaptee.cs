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
            public const string CreateCustomer = "/customers";
            public const string UpdateCustomer = "/customers/{0}";
            public const string GetCustomer = "/customers/{0}";
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
                $"Post Create Customer Request {requestUri}",
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
                $"Post Create Customer Request {requestUri}",
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

        private void AddRequestHeaders()
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Remove("Authorization");

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Token token=\"{_appSettings.FlyBuy.TokenApiSecret}\"");
        }
    }
}
