using DXP.SmartConnectPickup.Common.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DXP.SmartConnectPickup.DataServices.Models
{
    public class Order : Entity<string>
    {
        public string OrderApiId { get; set; }

        public string Provider { get; set; }

        public string ExternalId { get; set; }

        public string ExternalStatus { get; set; }

        public string RedemptionCode { get; set; }

        public string RedemptionUrl { get; set; }

        public string ExternalSiteId { get; set; }

        public string PickupType { get; set; }

        public string PickupWindow { get; set; }

        public bool? IsSync { get; set; }

        public string DisplayId { get; set; }

        public string StoreService { get; set; }

        public string StoreId { get; set; }

        public string OrderStatus { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

        // More Infor

        [NotMapped]
        public string CustomerPhone { get; set; }

        [NotMapped]
        public string CustomerName { get; set; }

        [NotMapped]
        public string CustomerToken { get; set; }

        [NotMapped]
        public string AppAuthorizationToken { get; set; }

        [NotMapped]
        public string CustomerCarColor { get; set; }

        [NotMapped]
        public string CustomerCarType { get; set; }

        [NotMapped]
        public string CustomerLicensePlate { get; set; }

        [NotMapped]
        public string PartnerIdentifier { get; set; }

        [NotMapped]
        public string PartnerIdentifierForCrew { get; set; }

        [NotMapped]
        public string PartnerIdentifierForCustomer { get; set; }

        [NotMapped]
        public string PushToken { get; set; }
    }
}
