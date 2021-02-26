using DXP.SmartConnectPickup.BusinessServices.Models;
using DXP.SmartConnectPickup.Common.Models;
using System.Threading.Tasks;

namespace DXP.SmartConnectPickup.BusinessServices.Interfaces
{
    public interface IOrderService
    {
        Task<BaseResponseObject> GetOrderByOfferApiId(string orderApiId, bool isViaMerchant = false);
        Task<BaseResponseObject> CreateOrderAsync(OrderModel model);
        Task<BaseResponseObject> UpdateOrderAsync(OrderModel model);
        Task<BaseResponseObject> RetryUpdateOrderMerchantAsync(string token, string orderApiId);
        Task<BaseResponseObject> RetryUpdateMutilOrderMerchantAsync(string token, int length = 10, int skipIndex = 1);

        Task<BaseResponseObject> ChangeStateOrderEvent(OrderModel model);
    }
}
