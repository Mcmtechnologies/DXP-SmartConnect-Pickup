using DXP.SmartConnectPickup.BusinessServices.Interfaces;
using DXP.SmartConnectPickup.BusinessServices.Models;
using DXP.SmartConnectPickup.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;


namespace DXP.SmartConnectPickup.API.Controllers
{
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _orderService;
        /// <summary>
        /// Constructor for BaseApiControllers.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="transactionLogService">The transactionLogService.</param>
        public OrderController(ILogger<BaseApiController> logger,
            ITransactionLogService transactionLogService,
            IOrderService orderService)
            : base(logger, transactionLogService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Gets Order by Order Api Id.
        /// </summary>
        /// <param name="orderApiId">The orderApiId.</param>
        /// <param name="isViaMerchant">The isViaMerchant, Get Order via Merchant.</param>
        /// <returns>BaseResponseObject.</returns>
        [HttpGet("getorderbyofferapiid")]
        public async Task<BaseResponseObject> GetOrderByOfferApiId(string orderApiId, bool isViaMerchant = false)
        {
            return await _orderService.GetOrderByOfferApiId(orderApiId, isViaMerchant);
        }

        /// <summary>
        /// Creates a Order.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>BaseResponseObject.</returns>
        [HttpPost("createorder")]
        public async Task<BaseResponseObject> CreateOrder(OrderModel model)
        {
            return await _orderService.CreateOrderAsync(model);
        }

        /// <summary>
        /// Update a Order.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>BaseResponseObject.</returns>
        [HttpPut("updateorder")]
        public async Task<BaseResponseObject> UpdateOrder(OrderModel model)
        {
            return await _orderService.UpdateOrderAsync(model);
        }

        /// <summary>
        /// Retry Update Order Merchant By OrderApi Id.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="orderApiId">The orderApiId.</param>
        /// <returns>BaseResponseObject.</returns>
        [HttpPost("retryupdateordermerchantbyuserid")]
        public async Task<BaseResponseObject> RetryUpdateOrderMerchantByUserId(string token, string orderApiId)
        {
            return await _orderService.RetryUpdateOrderMerchantAsync(token, orderApiId);
        }

        /// <summary>
        /// Retry Update Mutil Order Merchant.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="length">The number of Order retry.</param>
        /// <param name="skipIndex">The skipIndex.</param>
        /// <returns>BaseResponseObject.</returns>
        [HttpPost("retryupdatemutilordermerchant")]
        public async Task<BaseResponseObject> RetryUpdateMutilOrderMerchant(string token, int length = 10, int skipIndex = 1)
        {
            return await _orderService.RetryUpdateMutilOrderMerchantAsync(token, length, skipIndex);
        }

        /// <summary>
        ///Change State Order Event.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>BaseResponseObject.</returns>
        [HttpPost("changestateorderevent")]
        public async Task<BaseResponseObject> ChangeStateOrderEvent(OrderModel model)
        {
            return await _orderService.ChangeStateOrderEvent(model);
        }
    }
}
