using DXP.SmartConnectPickup.Common.Repository;
using DXP.SmartConnectPickup.DataServices.Models;
using System.Threading.Tasks;

namespace DXP.SmartConnectPickup.DataServices.Interfaces
{
    public interface ISiteRepository : IGenericRepository<Site, string>
    {
        Task<Site> GetSiteByStoreIdAndProvider(string storeId, string provider);
    }
}
