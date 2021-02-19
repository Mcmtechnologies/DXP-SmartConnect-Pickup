using Newtonsoft.Json;

namespace DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters.FlyBuy
{
    public class FlyBuyCustomerResponseData
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "api_token")]
        public string ApiToken { get; set; }

        [JsonProperty(PropertyName = "car_color")]
        public string CarColor { get; set; }

        [JsonProperty(PropertyName = "car_type")]
        public string CarType { get; set; }

        [JsonProperty(PropertyName = "license_plate")]
        public string LisensePlate { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "partner_identifier")]
        public string PartnerIdentifier { get; set; }
    }
}
