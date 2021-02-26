using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters.FlyBuy
{
    public class FlyBuyGetOrderRequest
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
    }
}
