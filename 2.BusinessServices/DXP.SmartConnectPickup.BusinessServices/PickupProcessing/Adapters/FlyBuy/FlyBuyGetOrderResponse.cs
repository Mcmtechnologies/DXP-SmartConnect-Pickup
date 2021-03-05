using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters.FlyBuy
{
    public class FlyBuyGetOrderResponse : FlyBuyResponseBase
    {
        [JsonProperty(PropertyName = "data")]
        public FlyBuyOrderResponseData Data { get; set; }
    }
}
