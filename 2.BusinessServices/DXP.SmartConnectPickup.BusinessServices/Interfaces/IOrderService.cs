using DXP.SmartConnectPickup.BusinessServices.Models;
using DXP.SmartConnectPickup.Common.Models;
using System.Threading.Tasks;

namespace DXP.SmartConnectPickup.BusinessServices.Interfaces
{
    public interface IOrderService
    {
        Task<BaseResponseObject> GetOrderByUserId(string orderApiId, bool isViaMerchant = false);
        Task<BaseResponseObject> CreateOrderAsync(OrderModel model);
        Task<BaseResponseObject> UpdateOrderAsync(OrderModel model);
    }
}
