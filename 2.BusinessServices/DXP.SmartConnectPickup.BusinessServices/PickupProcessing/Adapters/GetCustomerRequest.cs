using DXP.SmartConnectPickup.Common.Models;

namespace DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters
{
    public class GetCustomerRequest : BaseRequestObject
    {
        public string Id { get; set; }

        public string ExternalId { get; set; }
    }
}
