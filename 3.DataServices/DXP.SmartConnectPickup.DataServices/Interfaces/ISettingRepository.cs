using DXP.SmartConnectPickup.Common.Repository;
using DXP.SmartConnectPickup.DataServices.Models;

namespace DXP.SmartConnectPickup.DataServices.Interfaces
{
    public interface ISettingRepository : IGenericRepository<Setting, int>
    {
        Setting GetByName(string settingName);
    }
}
