using DXP.SmartConnectPickup.DataServices.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DXP.SmartConnectPickup.BusinessServices.Interfaces
{
    public interface IService_Service
    {
        Task<List<Service>> GetAllStoreServices();
        Task<Service> GetStoreServicesById(string id);
    }
}
