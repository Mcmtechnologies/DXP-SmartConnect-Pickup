using System;

namespace DXP.SmartConnectPickup.BusinessServices.Models
{
    public class CustomerFlyBuyViewModel
    {
        public string UserId { get; set; }

        public string Id { get; set; }

        public string ApiToken { get; set; }

        public string CarColor { get; set; }

        public string CarType { get; set; }

        public string LisensePlate { get; set; }

        public string PartnerIdentifier { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public bool? IsSync { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }
    }
}
