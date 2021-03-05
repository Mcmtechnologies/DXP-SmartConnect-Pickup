using DXP.SmartConnectPickup.Common.Repository;
using DXP.SmartConnectPickup.DataServices.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DXP.SmartConnectPickup.DataServices.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order, string>
    {
        Task<Order> GetOrderByOrderApiIdAsync(string orderApiId);
        Task<IEnumerable<Order>> GetOrderNotSyncByProviderAsync(string provider, int pagesize = 10, int index = 1);
    }
}
