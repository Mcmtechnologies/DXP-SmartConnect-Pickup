using DXP.SmartConnectPickup.Common.Models;

namespace DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters
{
    public class UpdateCustomerRequest : BaseRequestObject
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string ExternalId { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string CarColor { get; set; }

        public string CarType { get; set; }

        public string LisensePlate { get; set; }

        public bool? TermsOfService { get; set; }

        public bool? AgeVerification { get; set; }
    }
}
