using DXP.SmartConnectPickup.BusinessServices.Models;
using DXP.SmartConnectPickup.Common.Models;
using System.Threading.Tasks;

namespace DXP.SmartConnectPickup.BusinessServices.Interfaces
{
    public interface ICustomerService
    {
        Task<BaseResponseObject> GetCustomerByUserId(string userId, bool isViaMerchant = false);
        Task<BaseResponseObject> CreateCustomerAsync(CustomerFlyBuyModel model);
        Task<BaseResponseObject> UpdateCustomerAsync(CustomerFlyBuyModel model);
        Task<BaseResponseObject> RetryUpdateCustomerMerchantAsync(string token, string userId);
        Task<BaseResponseObject> RetryUpdateMutilCustomerMerchantAsync(string token, int length = 10, int skipIndex = 1);
    }
}
