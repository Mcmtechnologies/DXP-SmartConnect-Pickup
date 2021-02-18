using Newtonsoft.Json;

namespace DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters.FlyBuy
{
    public class FlyBuyCustomerRequestData
    {
        [JsonProperty(PropertyName = "car_color")]
        public string CarColor { get; set; }

        [JsonProperty(PropertyName = "car_type")]
        public string CarType { get; set; }

        [JsonProperty(PropertyName = "license_plate")]
        public string LisensePlate { get; set; }

        [JsonProperty(PropertyName = "partner_identifier")]
        public string PartnerIdentifier { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "terms_of_service")]
        public bool TermsOfService { get; set; }

        [JsonProperty(PropertyName = "age_verification")]
        public bool AgeVerification { get; set; }
    }
}
