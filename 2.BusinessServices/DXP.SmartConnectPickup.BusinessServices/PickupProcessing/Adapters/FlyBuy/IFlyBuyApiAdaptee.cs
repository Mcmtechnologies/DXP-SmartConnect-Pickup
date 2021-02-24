using System.Threading.Tasks;

namespace DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters.FlyBuy
{
    public interface IFlyBuyApiAdaptee
    {
        Task<FlyBuyCreateCustomerResponse> CreateCustomerAsync(FlyBuyCreateCustomerRequest request, string correlationId);
        Task<FlyBuyUpdateCustomerResponse> UpdateCustomerAsync(FlyBuyUpdateCustomerRequest request, string correlationId);
        Task<FlyBuyGetCustomerResponse> GetCustomerAsync(FlyBuyGetCustomerRequest request, string correlationId);

        Task<FlyBuyCreateOrderResponse> CreateOrderAsync(FlyBuyCreateOrderRequest request, string correlationId);
        Task<FlyBuyUpdateOrderResponse> UpdateOrderAsync(FlyBuyUpdateOrderRequest request, string correlationId);
        Task<FlyBuyGetOrderResponse> GetOrderAsync(FlyBuyGetOrderRequest request, string correlationId);
        Task<FlyBuyChangeStateOrderResponse> ChangeStateOrder(FlyBuyChangeStateOrderRequest request, string correlationId);
        Task<FlyBuyCustomerRatingOrderResponse> CustomerRatingOrder(FlyBuyCustomerRatingOrderRequest request, string correlationId);
    }
}
