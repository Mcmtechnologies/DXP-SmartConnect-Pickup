using DXP.SmartConnectPickup.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
