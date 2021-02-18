using DXP.SmartConnectPickup.BusinessServices.Interfaces;
using DXP.SmartConnectPickup.BusinessServices.Models;
using DXP.SmartConnectPickup.DataServices.Interfaces;
using DXP.SmartConnectPickup.DataServices.Models;
using Mapster;
using System.Collections.Generic;
using System.Linq;

namespace DXP.SmartConnectPickup.BusinessServices.Services
{
    public class SettingService : ISettingService
    {
        private readonly ISettingRepository _settingRepository;

        public SettingService(ISettingRepository settingRepository)
        {
            _settingRepository = settingRepository;
        }

        /// <summary>
        /// Get Setting.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>SettingModel.</returns>
        public SettingModel GetSetting(int id)
        {
            Setting Setting = _settingRepository.Get(id);
            return Setting.Adapt<SettingModel>();
        }

        /// <summary>
        /// GetSettingValueByName.
        /// </summary>
        /// <param name="name">the name.</param>
        /// <returns>string.</returns>
        public string GetSettingValueByName(string name)
        {
            Setting Setting = _settingRepository.GetByName(name);

            if (Setting != null)
            {
                return Setting.SettingValue;
            }

            return null;
        }

        /// <summary>
        /// Get All Setting.
        /// </summary>
        /// <returns>IList{SettingModel}.</returns>
        public IList<SettingModel> GetAllSetting()
        {
            IList<Setting> Settings = _settingRepository.GetAll().ToList();
            return Settings.Adapt<IList<SettingModel>>();
        }

        /// <summary>
        /// Get Setting By Name.
        /// </summary>
        /// <param name="name">the name.</param>
        /// <returns>SettingModel.</returns>
        public SettingModel GetSettingByName(string name)
        {
            Setting Setting = _settingRepository.GetByName(name);
            return Setting.Adapt<SettingModel>();
        }
    }
}
