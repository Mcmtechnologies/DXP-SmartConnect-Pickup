using DXP.SmartConnectPickup.Common.Repository;
using DXP.SmartConnectPickup.DataServices.Context;
using DXP.SmartConnectPickup.DataServices.Interfaces;
using DXP.SmartConnectPickup.DataServices.Models;
using System.Linq;

namespace DXP.SmartConnectPickup.DataServices.Repositories
{
    public class SettingRepository : GenericRepository<DBContext, Setting, int>, ISettingRepository
    {
        public SettingRepository(DBContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Get By Name.
        /// </summary>
        /// <param name="settingName">The settingName.</param>
        /// <returns>Setting</returns>
        public Setting GetByName(string settingName)
        {
            settingName = settingName.Trim().ToLower();
            return _dbContext.Setting.FirstOrDefault(e => e.SettingName.ToLower() == settingName);
        }
    }
}
