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
    }
}
