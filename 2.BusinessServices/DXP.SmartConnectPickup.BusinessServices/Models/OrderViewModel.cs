﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DXP.SmartConnectPickup.BusinessServices.Models
{
    public class OrderViewModel
    {
        public string Id { get; set; }

        public string OrderApiId { get; set; }

        public string Provider { get; set; }

        public string ExternalId { get; set; }

        public string ExternalStatus { get; set; }

        public string RedemptionCode { get; set; }

        public string RedemptionUrl { get; set; }

        public string ExternalSiteId { get; set; }

        public bool? IsSync { get; set; }

        public string DisplayId { get; set; }

        public string StoreService { get; set; }

        public string StoreId { get; set; }

        public string OrderStatus { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

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