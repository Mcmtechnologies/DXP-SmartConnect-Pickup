using DXP.SmartConnectPickup.BusinessServices.Interfaces;
using DXP.SmartConnectPickup.BusinessServices.Models;
using DXP.SmartConnectPickup.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DXP.SmartConnectPickup.API.Controllers
{
    public class CustomerController : BaseApiController
    {
        private readonly ICustomerService _customerService;
        /// <summary>
        /// Constructor for BaseApiControllers.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="transactionLogService">The transactionLogService.</param>
        public CustomerController(ILogger<BaseApiController> logger,
            ITransactionLogService transactionLogService,
            ICustomerService customerService)
            : base(logger, transactionLogService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Gets customer by user Id.
        /// </summary>
        /// <param name="userId">The userId.</param>
        /// <param name="isViaMerchant">The isViaMerchant, Get customer via Merchant.</param>
        /// <returns>BaseResponseObject.</returns>
        [HttpGet("getcustomerbyuserid")]
        public async Task<BaseResponseObject> GetCustomerByUserId(string userId, bool isViaMerchant = false)
        {
            return await _customerService.GetCustomerByUserId(userId, isViaMerchant);
        }

        /// <summary>
        /// Creates a Customer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>BaseResponseObject.</returns>
        [HttpPost("createcustomer")]
        public async Task<BaseResponseObject> CreateCustomer(CustomerModel model)
        {
            return await _customerService.CreateCustomerAsync(model);
        }

        /// <summary>
        /// Update a Customer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>BaseResponseObject.</returns>
        [HttpPut("updatecustomer")]
        public async Task<BaseResponseObject> UpdateCustomer(CustomerModel model)
        {
            return await _customerService.UpdateCustomerAsync(model);
        }

        /// <summary>
        /// Retry Update Customer Merchant By User Id.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="userId">The userId.</param>
        /// <returns>BaseResponseObject.</returns>
        [HttpPost("retryupdatecustomermerchantbyuserid")]
        public async Task<BaseResponseObject> RetryUpdateCustomerMerchantByUserId(string token, string userId)
        {
            return await _customerService.RetryUpdateCustomerMerchantAsync(token, userId);
        }

        /// <summary>
        /// Retry Update Mutil Customer Merchant.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="length">The number of customer retry.</param>
        /// <param name="skipIndex">The skipIndex.</param>
        /// <returns>BaseResponseObject.</returns>
        [HttpPost("retryupdatemutilcustomermerchant")]
        public async Task<BaseResponseObject> RetryUpdateMutilCustomerMerchant(string token, int length = 10, int skipIndex = 1)
        {
            return await _customerService.RetryUpdateMutilCustomerMerchantAsync(token, length, skipIndex);
        }
    }
}
