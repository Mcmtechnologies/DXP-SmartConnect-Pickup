using DXP.SmartConnectPickup.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters
{
    public class GetOrderRequest : BaseRequestObject
    {
        public string Id { get; set; }

        public string ExternalId { get; set; }
    }
}
