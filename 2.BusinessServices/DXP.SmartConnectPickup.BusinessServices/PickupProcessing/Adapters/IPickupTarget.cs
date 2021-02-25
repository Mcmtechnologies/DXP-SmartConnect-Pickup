using System;
using System.Threading.Tasks;

namespace DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters
{
    public interface IPickupTarget
    {
        Task<CreateCustomerResponse> CreateCustomerAsync(CreateCustomerRequest request, Guid correlationId);
        Task<CreateCustomerResponse> CreateCustomerAsync(CreateCustomerRequest request, string correlationId);

        Task<UpdateCustomerResponse> UpdateCustomerAsync(UpdateCustomerRequest request, Guid correlationId);
        Task<UpdateCustomerResponse> UpdateCustomerAsync(UpdateCustomerRequest request, string correlationId);

        Task<GetCustomerResponse> GetCustomerAsync(GetCustomerRequest request, Guid correlationId);
        Task<GetCustomerResponse> GetCustomerAsync(GetCustomerRequest request, string correlationId);

        Task<CreateOrderResponse> CreateOrderAsync(CreateOrderRequest request, Guid correlationId);
        Task<CreateOrderResponse> CreateOrderAsync(CreateOrderRequest request, string correlationId);

        Task<UpdateOrderResponse> UpdateOrderAsync(UpdateOrderRequest request, Guid correlationId);
        Task<UpdateOrderResponse> UpdateOrderAsync(UpdateOrderRequest request, string correlationId);

        Task<GetOrderResponse> GetOrderAsync(GetOrderRequest request, Guid correlationId);
        Task<GetOrderResponse> GetOrderAsync(GetOrderRequest request, string correlationId);

        Task<ChangeStateOrderResponse> ChangeStateOrder(ChangeStateOrderRequest request, Guid correlationId);
        Task<ChangeStateOrderResponse> ChangeStateOrder(ChangeStateOrderRequest request, string correlationId);
    }
}
