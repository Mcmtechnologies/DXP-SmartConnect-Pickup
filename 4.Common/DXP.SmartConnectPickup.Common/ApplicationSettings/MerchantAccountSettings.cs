namespace DXP.SmartConnectPickup.Common.ApplicationSettings
{
    public class MerchantAccountSettings
    {
        public string PickupProviderDefault { get; set; }
        public int MaxNumberCustomerRetry { get; set; }
        public string TokenRetry { get; set; }
        public FlyBuyApi FlyBuy { get; set; }
    }

    public class FlyBuyApi : MerchantAccountApiBase
    {
        public string TokenApiSecret { get; set; }
    }

    public class MerchantAccountApiBase
    {
        public string ProviderName { get; set; }

        public string ApiUrl { get; set; }
    }
}
