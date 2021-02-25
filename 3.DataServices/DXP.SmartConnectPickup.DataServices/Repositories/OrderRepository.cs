using DXP.SmartConnectPickup.Common.Repository;
using DXP.SmartConnectPickup.DataServices.Context;
using DXP.SmartConnectPickup.DataServices.Interfaces;
using DXP.SmartConnectPickup.DataServices.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DXP.SmartConnectPickup.DataServices.Repositories
{
    public class OrderRepository : GenericRepository<DBContext, Order, string>, IOrderRepository
    {
        public OrderRepository(DBContext dbContext) : base(dbContext)
        {
        }

        public async Task<Order> GetOrderByOrderApiIdAsync(string orderApiId)
        {
            return await _dbContext.Order.FirstOrDefaultAsync(x => x.OrderApiId == orderApiId);
        }

        /// <summary>
        /// Gets Order Not Sync By Provider Async.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="pagesize">The pagesize.</param>
        /// <param name="index">The index.</param>
        /// <returns>Task{IEnumerable{Order}}</returns>
        public async Task<IEnumerable<Order>> GetOrderNotSyncByProviderAsync(string provider, int pagesize = 10, int index = 1)
        {
            return await _dbContext.Order.Where(x => x.IsSync == false && x.Provider == provider)
                .Skip((index - 1) * pagesize)
                .Take(pagesize)
                .ToArrayAsync();
        }
    }
}
