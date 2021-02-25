using DXP.SmartConnectPickup.DataServices.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DXP.SmartConnectPickup.BusinessServices.Interfaces
{
    public interface IService_Service
    {
        Task<List<Service>> GetAllServicesAsync();
        Task<Service> GetServicesByIdAsync(string id);
    }
}
