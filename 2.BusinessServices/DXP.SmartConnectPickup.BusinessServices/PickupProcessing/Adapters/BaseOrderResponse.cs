using DXP.SmartConnectPickup.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters
{
    public class BaseOrderResponse : BasePickupResponse
    {
        public string OrderDisplayId { get; set; }

        public string ExternalId { get; set; }

        public string ExternalSiteId { get; set; }

        public string RedemptionCode { get; set; }

        public string RedemptionUrl { get; set; }

        public string OrderState { get; set; }

        // More infor 

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

        public string CustomerRatingValue { get; set; }

        public string CustomerRatingValueString { get; set; }

        public string CustomerRatingComments { get; set; }

        public string PickupWindow { get; set; }

        public string PickupType { get; set; }

        public string PushToken { get; set; }
    }
}
