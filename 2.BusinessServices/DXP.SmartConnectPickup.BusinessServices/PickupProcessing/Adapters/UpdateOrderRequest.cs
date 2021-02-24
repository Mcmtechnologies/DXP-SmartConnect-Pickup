using DXP.SmartConnectPickup.Common.Models;

namespace DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters
{
    public class UpdateOrderRequest : BaseRequestObject
    {
        public string Id { get; set; }
        public string ExternalId { get; set; }
        public string DisplayId { get; set; }
        public string SiteId { get; set; }
        public string PickupWindow { get; set; }
        public string PickupType { get; set; }
        public string State { get; set; }
    }
}
