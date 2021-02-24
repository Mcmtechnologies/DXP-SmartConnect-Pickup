using DXP.SmartConnectPickup.DataServices.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DXP.SmartConnectPickup.BusinessServices.Interfaces
{
    public interface IStoreService_Service
    {
        Task<List<StoreService>> GetAllStoreServices();
        Task<StoreService> GetStoreServicesById(string id);
    }
}
