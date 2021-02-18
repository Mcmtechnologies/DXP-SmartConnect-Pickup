using DXP.SmartConnectPickup.Common.ApplicationSettings;
using DXP.SmartConnectPickup.Common.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DXP.SmartConnectPickup.API.Controllers
{
    /// <summary>
    /// The AppController.
    /// </summary>
    [AllowAnonymous]
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly ReadmeSettings _configuration;

        /// <summary>
        /// The ProgramController constructor.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="config">The config.</param>
        public AppController(ILogger<AppController> logger,
            IOptions<ReadmeSettings> config)
        {
            _configuration = config.Value;
        }

        /// <summary>
        /// Returns Readme information.
        /// </summary>
        /// <returns>Dynamic.</returns>
        [Route("readme")]
        [HttpGet]
        public dynamic Readme()
        {
            return new
            {
                Server = $"{System.Net.Dns.GetHostName()}",
                BuildNumber = _configuration.BuildNumber,
                BuildDate = _configuration.BuildDate,
                Commit = _configuration.Commit,
                Branch = _configuration.Branch
            };
        }

        /// <summary>
        /// Gets upc12 from upc11.
        /// </summary>
        /// <param name="upc11">The upc11.</param>
        /// <returns>System.String.</returns>
        [Route("GetUpc12")]
        [HttpGet]
        public string GetUpc12(string upc11)
        {
            return BarCodeUtils.GetUpc12(upc11);
        }

        /// <summary>
        /// Gets upc13 from upc12.
        /// </summary>
        /// <param name="upc12">The upc12.</param>
        /// <returns>System.String.</returns>
        [Route("GetUpc13")]
        [HttpGet]
        public string GetUpc13(string upc12)
        {
            return BarCodeUtils.GetUpc13(upc12);
        }

        /// <summary>
        /// Gets barcode.
        /// </summary>
        /// <param name="upc">The upc.</param>
        /// <param name="finalRetailPrice">The finalRetailPrice.</param>
        /// <returns>System.String.</returns>
        [Route("GetBarcode")]
        [HttpGet]
        public string GetBarcode(string upc, decimal finalRetailPrice)
        {
            return BarCodeUtils.GetBarcode(upc, finalRetailPrice);
        }
    }
}
