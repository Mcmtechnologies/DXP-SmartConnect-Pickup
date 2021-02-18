using DXP.SmartConnectPickup.BusinessServices.Interfaces;
using DXP.SmartConnectPickup.BusinessServices.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DXP.SmartConnectPickup.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingController : BaseApiController
    {
        private readonly ISettingService _settingAppService;

        public SettingController(ISettingService settingAppService,
            ILogger<SettingController> logger,
            ITransactionLogService transactionLogService)
            : base(logger, transactionLogService)
        {
            _settingAppService = settingAppService;
        }

        /// <summary>
        /// Gets Setting By Id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>SettingDto.</returns>
        [HttpGet("by-id/{id}")]
        public SettingModel GetSetting(int id)
        {
            return _settingAppService.GetSetting(id);
        }

        /// <summary>
        /// Gets Setting Value by Name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The SettingDto.</returns>
        [HttpGet("by-name/{name}")]
        public SettingModel GetSettingByyName(string name)
        {
            return _settingAppService.GetSettingByName(name);
        }

        /// <summary>
        /// Gets Setting Value by Name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>System.String.</returns>
        [HttpGet("value-by-name/{name}")]
        public string GetSettingValueByName(string name)
        {
            return _settingAppService.GetSettingValueByName(name);
        }

        /// <summary>
        /// Gets List Type by Setting Name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>IList{TypeDto}.</returns>
        [HttpGet("type/{name}")]
        public IList<SettingTypeModel> GetSettingType(string name)
        {
            string settingByName = _settingAppService.GetSettingValueByName(name);
            IList<SettingTypeModel> types = JsonConvert.DeserializeObject<List<SettingTypeModel>>(settingByName);
            return types;
        }

        /// <summary>
        /// Gets all settings.
        /// </summary>
        /// <returns>IList{SettingDto}.</returns>
        [HttpGet("all")]
        public IList<SettingModel> GetAllSetting()
        {
            return _settingAppService.GetAllSetting();
        }
    }
}
