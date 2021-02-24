using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters.FlyBuy
{
    public class FlyBuyCreateOrderRequest
    {
        [JsonProperty(PropertyName = "data")]
        public FlyBuyOrderRequestData Data { get; set; }
    }
}
