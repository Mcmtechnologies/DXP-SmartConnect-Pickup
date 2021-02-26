using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters.FlyBuy
{
    public class FlyBuyOrderEventStateChangeRequestData
    {
        [JsonProperty(PropertyName = "order_id")]
        public string OrderId { get; set; }

        [JsonProperty(PropertyName = "event_type")]
        public string EventType { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }
    }
}
