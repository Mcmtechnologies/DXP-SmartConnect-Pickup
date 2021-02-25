using DXP.SmartConnectPickup.BusinessServices.Interfaces;
using DXP.SmartConnectPickup.BusinessServices.Models;
using DXP.SmartConnectPickup.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DXP.SmartConnectPickup.API.Controllers
{
    public class SiteController : BaseApiController
    {
        private readonly ISiteService _siteService;
        /// <summary>
        /// Constructor for BaseApiControllers.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="transactionLogService">The transactionLogService.</param>
        public SiteController(ILogger<BaseApiController> logger,
            ITransactionLogService transactionLogService,
            ISiteService siteService)
            : base(logger, transactionLogService)
        {
            _siteService = siteService;
        }

        /// <summary>
        /// Gets Site by Store Api Id.
        /// </summary>
        /// <param name="storeId">The storeApiId.</param>
        /// <returns>BaseResponseObject.</returns>
        [HttpGet("getSitebyStoreApiId")]
        public async Task<BaseResponseObject> GetSiteByOfferApiId(string storeApiId)
        {
            return await _siteService.GetSiteByStoreApiId(storeApiId);
        }

        /// <summary>
        /// Creates a Site.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>BaseResponseObject.</returns>
        [HttpPost("createSite")]
        public async Task<BaseResponseObject> CreateSite(SiteModel model)
        {
            return await _siteService.CreateSiteAsync(model);
        }

        /// <summary>
        /// Update a Site.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>BaseResponseObject.</returns>
        [HttpPut("updateSite")]
        public async Task<BaseResponseObject> UpdateSite(SiteModel model)
        {
            return await _siteService.UpdateSiteAsync(model);
        }
    }
}
