using Newtonsoft.Json;

namespace DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters.FlyBuy
{
    public class FlyBuyGetCustomerRequest
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
    }
}
