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
    public class CustomerService : GenericService, ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationSettings _applicationSettings;
        private readonly MerchantAccountSettings _merchantAccountSettings;
        private readonly ITransactionLogService _transactionLogService;
        private readonly ICustomerRepository _customerRepository;

        private readonly IPickupAdapterFactory _pickupAdapterFactory;
        public CustomerService(
            IMapper mapper,
            IOptions<ApplicationSettings> applicationSettings,
            IOptions<MerchantAccountSettings> merchantAccountSettings,
            ITransactionLogService transactionLogService,
            ICustomerRepository customerRepository,
            IPickupAdapterFactory pickupAdapterFactory)
        {
            _mapper = mapper;
            _applicationSettings = applicationSettings.Value;
            _merchantAccountSettings = merchantAccountSettings.Value;
            _transactionLogService = transactionLogService;
            _customerRepository = customerRepository;
            _pickupAdapterFactory = pickupAdapterFactory;
        }

        /// <summary>
        /// Gets Customer By User Id.
        /// </summary>
        /// <param name="userId">The userId.</param>
        /// <param name="isViaMerchant">The isViaMerchant.</param>
        /// <returns>Task{BaseResponseObject}.</returns>
        public async Task<BaseResponseObject> GetCustomerByUserId(string userId, bool isViaMerchant = false)
        {
            Customer customer = await _customerRepository.GetCustomerByUserIdAndProviderAsync(userId, _merchantAccountSettings.PickupProviderDefault);

            if (customer != null && !string.IsNullOrEmpty(customer.ExternalId) && isViaMerchant)
            {
                // Builds pickup adapter
                IPickupTarget pickupTarget = PickupHelper.BuildPickupAdapter(_pickupAdapterFactory, _merchantAccountSettings.PickupProviderDefault);
                BaseRequestObject request = _mapper.Map<GetCustomerRequest>(customer);
                BaseCustomerResponse response = await pickupTarget.GetCustomerAsync((GetCustomerRequest)request, request.GetCorrelationId());

                await PickupHelper.HandleErrorResponse(_transactionLogService, request, response, TransactionLogStep.GetCustomer);

                // Set Sync = null when overwrite
                if (!string.IsNullOrEmpty(response.ExternalId))
                {
                    customer.IsSync = null;
                    customer.CarColor = response.CarColor;
                    customer.CarType = response.CarType;
                    customer.LisensePlate = response.LisensePlate;
                    customer.Phone = response.Phone;
                    customer.Name = response.Name;
                    customer.ExternalApiToken = response.ExternalApiToken;
                }
            }

            return ReturnSuccess(_mapper.Map<Customer, CustomerViewModel>(customer));
        }

        /// <summary>
        /// Creates a Customer Async.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task{BaseResponseObject}.</returns>
        public async Task<BaseResponseObject> CreateCustomerAsync(CustomerModel model)
        {
            Guard.AgainstNullOrEmpty(nameof(model.Name), model.Name);
            Guard.AgainstInvalidArgumentWithMessage($"{nameof(model.AgeVerification)} must be accepted", model.AgeVerification ?? false);
            Guard.AgainstInvalidArgumentWithMessage($"{nameof(model.TermsOfService)} must be accepted", model.TermsOfService ?? false);

            Customer existCustomer = await _customerRepository.GetCustomerByUserIdAndProviderAsync(model.UserId, _merchantAccountSettings.PickupProviderDefault);

            Guard.AgainstInvalidArgumentWithMessage($"Customer is already created.", existCustomer == null);

            Customer customer = model.Adapt<Customer>();

            customer.Id = Guid.NewGuid().ToString();
            customer.Provider = _merchantAccountSettings.PickupProviderDefault;
            customer.IsSync = false;
            customer.ExternalId = null;
            customer.ExternalApiToken = null;
            customer.TermsOfService = model.TermsOfService;
            customer.AgeVerification = model.AgeVerification;
            customer.CreatedBy = AuthorizationConstants.SITE_ADMIN_ROLE;
            customer.CreatedDate = DateTime.UtcNow;
            customer.ModifiedBy = AuthorizationConstants.SITE_ADMIN_ROLE;
            customer.ModifiedDate = DateTime.UtcNow;

            await _customerRepository.AddAndSaveChangesAsync(customer);

            await UpdateCustomerMerchantAsync(customer);

            return ReturnSuccess(_mapper.Map<Customer, CustomerViewModel>(customer));
        }

        /// <summary>
        /// Update a Customer Async.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task{BaseResponseObject}.</returns>
        public async Task<BaseResponseObject> UpdateCustomerAsync(CustomerModel model)
        {
            Customer customer = await _customerRepository.GetCustomerByUserIdAndProviderAsync(model.UserId, _merchantAccountSettings.PickupProviderDefault);

            Guard.AgainstNotFound(nameof(model.UserId), customer);

            customer.CarColor = model.CarColor ?? customer.CarColor;
            customer.CarType = model.CarType ?? customer.CarType;
            customer.LisensePlate = model.LisensePlate ?? customer.LisensePlate;
            customer.Phone = model.Phone ?? customer.Phone;
            customer.Name = model.Name ?? customer.Name;
            customer.ModifiedBy = AuthorizationConstants.SITE_ADMIN_ROLE;
            customer.ModifiedDate = DateTime.UtcNow;
            customer.IsSync = false;

            await _customerRepository.UpdateAndSaveChangesAsync(customer);

            await UpdateCustomerMerchantAsync(customer);

            return ReturnSuccess(_mapper.Map<Customer, CustomerViewModel>(customer));
        }

        /// <summary>
        /// Retry Update a Customer Merchant Async.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="userId">The userId.</param>
        /// <returns>Task{BaseResponseObject}.</returns>
        public async Task<BaseResponseObject> RetryUpdateCustomerMerchantAsync(string token, string userId)
        {
            Guard.AgainstInvalidArgument(nameof(token), token == _merchantAccountSettings.TokenRetry);

            Customer customer = await _customerRepository.GetCustomerByUserIdAndProviderAsync(userId, _merchantAccountSettings.PickupProviderDefault);

            Guard.AgainstNotFound(nameof(userId), customer);

            BaseCustomerResponse response = await UpdateCustomerMerchantAsync(customer);

            if (!string.IsNullOrEmpty(response.ExternalId))
            {
                return ReturnSuccess(_mapper.Map<Customer, CustomerViewModel>(customer));
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
        /// Retry Update Mutil Customer Merchant Async.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="length">The length.</param>
        /// <param name="skipIndex">The skipIndex.</param>
        /// <returns>Task{BaseResponseObject}.</returns>
        public async Task<BaseResponseObject> RetryUpdateMutilCustomerMerchantAsync(string token, int length = 10, int skipIndex = 1)
        {
            Guard.AgainstInvalidArgument(nameof(token), token == _merchantAccountSettings.TokenRetry);

            length = length > _merchantAccountSettings.MaxNumberCustomerRetry ? _merchantAccountSettings.MaxNumberCustomerRetry : length;

            IEnumerable<Customer> customers = await _customerRepository
                .GetCustomerNotSyncByProviderAsync(_merchantAccountSettings.PickupProviderDefault, length, skipIndex);

            int totalSuccess = 0;
            foreach (var customer in customers)
            {
                BaseCustomerResponse response = await UpdateCustomerMerchantAsync(customer);

                if (!string.IsNullOrEmpty(response.ExternalId))
                {
                    totalSuccess++;
                }
            }

            return ReturnSuccess(null, $"Update Customer Merchant Sync Success {totalSuccess} of {length}");
        }

        private async Task<BaseCustomerResponse> UpdateCustomerMerchantAsync(Customer customer)
        {
            // Builds pickup adapter
            IPickupTarget pickupTarget = PickupHelper.BuildPickupAdapter(_pickupAdapterFactory, _merchantAccountSettings.PickupProviderDefault);

            TransactionLogStep transactionLogStep;
            BaseCustomerResponse response;
            BaseRequestObject request;

            // Case: add to flybuy not success
            if (string.IsNullOrEmpty(customer.ExternalId))
            {
                transactionLogStep = TransactionLogStep.CreateCustomer;
                request = _mapper.Map<CreateCustomerRequest>(customer);
                response = await pickupTarget.CreateCustomerAsync((CreateCustomerRequest)request, request.GetCorrelationId());
            }
            else
            {
                transactionLogStep = TransactionLogStep.UpdateCustomer;
                request = _mapper.Map<UpdateCustomerRequest>(customer);
                response = await pickupTarget.UpdateCustomerAsync((UpdateCustomerRequest)request, request.GetCorrelationId());
            }

            await UpdateExternalId(customer, response);
            await PickupHelper.HandleErrorResponse(_transactionLogService, request, response, transactionLogStep);

            return response;
        }

        private async Task UpdateExternalId<T>(Customer customer, T response) where T : BaseCustomerResponse
        {
            if (!string.IsNullOrEmpty(response.ExternalId))
            {
                customer.ExternalId = response.ExternalId;
                customer.ExternalApiToken = response.ExternalApiToken;
                customer.Phone = response.Phone;
                customer.IsSync = true;
                customer.ModifiedDate = DateTime.Now;
                customer.ModifiedBy = AuthorizationConstants.SITE_ADMIN_ROLE;

                await _customerRepository.UpdateAndSaveChangesAsync(customer);
            }
        }
    }
}
