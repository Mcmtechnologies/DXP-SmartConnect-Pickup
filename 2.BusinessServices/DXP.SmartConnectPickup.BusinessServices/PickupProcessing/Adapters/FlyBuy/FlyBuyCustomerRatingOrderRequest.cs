using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters.FlyBuy
{
    public class FlyBuyCustomerRatingOrderRequest
    {
        [JsonProperty(PropertyName = "data")]
        public FlyBuyOrderEventCustomerRatingRequestData Data { get; set; }
    }
}
