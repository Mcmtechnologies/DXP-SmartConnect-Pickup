using DXP.SmartConnectPickup.Common.Repository;
using DXP.SmartConnectPickup.DataServices.Context;
using DXP.SmartConnectPickup.DataServices.Interfaces;
using DXP.SmartConnectPickup.DataServices.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
    }
}
