using DXP.SmartConnectPickup.Common.Repository;
using DXP.SmartConnectPickup.DataServices.Context;
using DXP.SmartConnectPickup.DataServices.Interfaces;
using DXP.SmartConnectPickup.DataServices.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DXP.SmartConnectPickup.DataServices.Repositories
{
    public class SiteRepository : GenericRepository<DBContext, Site, string>, ISiteRepository
    {
        public SiteRepository(DBContext dbContext) : base(dbContext)
        {
        }

        public async Task<Site> GetSiteByStoreIdAndProvider(string storeId, string provider)
        {
            return await _dbContext.Site.FirstOrDefaultAsync(x => x.StoreId == storeId && x.Provider == provider);
        }
    }
}
