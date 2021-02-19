﻿using DXP.SmartConnectPickup.Common.Models;
using Newtonsoft.Json;

namespace DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters.FlyBuy
{
    public class FlyBuyResponseBase : BaseHttpResponse
    {
        [JsonProperty(PropertyName = "errors")]
        public object Error { get; set; }
    }
}
