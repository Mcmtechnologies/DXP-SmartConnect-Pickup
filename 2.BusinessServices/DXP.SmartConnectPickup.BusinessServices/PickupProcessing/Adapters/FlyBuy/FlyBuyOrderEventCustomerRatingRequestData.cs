using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters.FlyBuy
{
    public class FlyBuyOrderEventCustomerRatingRequestData
    {
        [JsonProperty(PropertyName = "order_id")]
        public string OrderId { get; set; }

        [JsonProperty(PropertyName = "event_type")]
        public string EventType { get; set; }

        [JsonProperty(PropertyName = "customer_rating_value")]
        public string CustomerRatingValue { get; set; }

        [JsonProperty(PropertyName = "customer_rating_comments")]
        public string CustomerratingComments { get; set; }
    }
}
