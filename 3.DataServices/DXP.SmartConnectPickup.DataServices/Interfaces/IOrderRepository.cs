using DXP.SmartConnectPickup.Common.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using DXP.SmartConnectPickup.DataServices.Models;
using System.Threading.Tasks;

namespace DXP.SmartConnectPickup.DataServices.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order, string>
    {
        Task<Order> GetOrderByOrderApiIdAsync(string orderApiId);
    }
}
