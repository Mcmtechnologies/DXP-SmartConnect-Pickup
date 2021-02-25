using DXP.SmartConnectPickup.BusinessServices.Models;
using DXP.SmartConnectPickup.Common.Models;
using System.Threading.Tasks;

namespace DXP.SmartConnectPickup.BusinessServices.Interfaces
{
    public interface ISiteService
    {
        Task<BaseResponseObject> GetSiteByStoreApiId(string storeApiId);
        Task<BaseResponseObject> CreateSiteAsync(SiteModel model);
        Task<BaseResponseObject> UpdateSiteAsync(SiteModel model);
    }
}
