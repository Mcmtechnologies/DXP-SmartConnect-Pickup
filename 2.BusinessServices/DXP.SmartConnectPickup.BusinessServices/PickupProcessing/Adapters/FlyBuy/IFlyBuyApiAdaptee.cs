using System.Threading.Tasks;

namespace DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters.FlyBuy
{
    public interface IFlyBuyApiAdaptee
    {
        Task<FlyBuyCreateCustomerResponse> CreateCustomerAsync(FlyBuyCreateCustomerRequest request, string correlationId);
        Task<FlyBuyUpdateCustomerResponse> UpdateCustomerAsync(FlyBuyUpdateCustomerRequest request, string correlationId);
        Task<FlyBuyGetCustomerResponse> GetCustomerAsync(FlyBuyGetCustomerRequest request, string correlationId);
    }
}
