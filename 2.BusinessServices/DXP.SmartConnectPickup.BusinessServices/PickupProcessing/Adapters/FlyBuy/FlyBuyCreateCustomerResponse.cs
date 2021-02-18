using Newtonsoft.Json;

namespace DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters.FlyBuy
{
    public class FlyBuyCreateCustomerResponse : FlyBuyResponseBase
    {
        [JsonProperty(PropertyName = "data")]
        public FlyBuyCustomerResponseData Data { get; set; }
    }
}
