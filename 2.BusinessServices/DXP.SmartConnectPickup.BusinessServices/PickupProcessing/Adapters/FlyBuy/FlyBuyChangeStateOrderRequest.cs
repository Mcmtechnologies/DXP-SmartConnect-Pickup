using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters.FlyBuy
{
    public class FlyBuyChangeStateOrderRequest
    {
        [JsonProperty(PropertyName = "data")]
        public FlyBuyOrderEventStateChangeRequestData Data { get; set; }
    }
}
