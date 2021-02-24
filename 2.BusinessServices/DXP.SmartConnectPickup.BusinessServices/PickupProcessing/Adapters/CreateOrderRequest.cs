using DXP.SmartConnectPickup.Common.Enums;
using DXP.SmartConnectPickup.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters
{
    public class CreateOrderRequest : BaseRequestObject
    {
        public string Id { get; set; }
        public string DisplayId { get; set; }
        public string SiteId { get; set; }
        public string PickupWindow { get; set; }
        public string PickupType { get; set; }
        public string State { get; set; }
    }
}
