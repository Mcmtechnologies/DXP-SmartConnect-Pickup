using DXP.SmartConnectPickup.BusinessServices.Models;
using System.Collections.Generic;

namespace DXP.SmartConnectPickup.BusinessServices.Interfaces
{
    public interface ISettingService
    {
        SettingModel GetSetting(int id);
        string GetSettingValueByName(string name);
        IList<SettingModel> GetAllSetting();
        SettingModel GetSettingByName(string name);
    }
}
