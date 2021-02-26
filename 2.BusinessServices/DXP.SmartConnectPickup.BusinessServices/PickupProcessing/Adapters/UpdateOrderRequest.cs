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

        //More Infor

        public string ArrivedAt { get; set; }

        public string CustomerState { get; set; }

        public string EtaAt { get; set; }

        public string PartnerDisplayIdentifier { get; set; }

        public string PartnerIdentifierForCrew { get; set; }

        public string PartnerIdentifierForCustomer { get; set; }

        public string CustomerId { get; set; }

        public string SitePartnerIdentifier { get; set; }

        public string CustomerName { get; set; }

        public string CustomerCarColor { get; set; }

        public string CustomerCarType { get; set; }

        public string CustomerLicensePlate { get; set; }

        public string PushToken { get; set; }
    }
}
