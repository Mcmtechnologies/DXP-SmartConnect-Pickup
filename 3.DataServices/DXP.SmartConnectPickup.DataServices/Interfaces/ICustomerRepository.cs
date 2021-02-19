using DXP.SmartConnectPickup.Common.Repository;
using DXP.SmartConnectPickup.DataServices.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DXP.SmartConnectPickup.DataServices.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customer, string>
    {
        Task<Customer> GetCustomerByUserIdAsync(string userId);
        Task<Customer> GetCustomerByUserIdAndProviderAsync(string userId, string provider);
        Task<IEnumerable<Customer>> GetCustomerNotSyncByProviderAsync(string provider, int pagesize = 10, int index = 1);
    }
}
