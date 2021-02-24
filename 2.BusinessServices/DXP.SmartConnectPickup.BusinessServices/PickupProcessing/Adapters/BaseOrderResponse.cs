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

        public string State { get; set; }
    }
}
