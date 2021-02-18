using System;

namespace DXP.SmartConnectPickup.BusinessServices.Models
{
    public class CustomerViewModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }

        public string Provider { get; set; }

        public string ExternalId { get; set; }

        public string ExternalApiToken { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string CarColor { get; set; }

        public string CarType { get; set; }

        public string LisensePlate { get; set; }

        public bool? TermsOfService { get; set; }

        public bool? AgeVerification { get; set; }

        public bool? IsSync { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }
    }
}
