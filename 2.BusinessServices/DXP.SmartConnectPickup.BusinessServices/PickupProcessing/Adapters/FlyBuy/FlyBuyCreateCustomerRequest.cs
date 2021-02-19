using Newtonsoft.Json;

namespace DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters.FlyBuy
{
    public class FlyBuyCreateCustomerRequest
    {
        [JsonProperty(PropertyName = "data")]
        public FlyBuyCustomerRequestData Data { get; set; }
    }
}
