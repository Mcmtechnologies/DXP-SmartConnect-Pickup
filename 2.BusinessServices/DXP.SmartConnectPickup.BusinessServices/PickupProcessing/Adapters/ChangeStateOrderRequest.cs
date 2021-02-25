using DXP.SmartConnectPickup.Common.Models;

namespace DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters
{
    public class ChangeStateOrderRequest : BaseRequestObject
    {
        public string Id { get; set; }
        public string ExternalId { get; set; }
        public string EventType { get; set; }
        public string State { get; set; }
    }
}
