namespace DXP.SmartConnectPickup.Common.ApplicationSettings
{
    public class ApplicationSettings
    {
        public string RootFolder { get; set; }
        public string StripeSecretKey { get; set; }
        public string StripeWebhookSecretKey { get; set; }
        public string SystemEmailBatchId { get; set; }
        public string EmailFromName { get; set; }
        public string DefaultTimeZone { get; set; }
        public string DefaultCompanyId { get; set; }
        public string StoreDBName { get; set; }
        public string ProductDBName { get; set; }
        public string BaseUrl { get; set; }
        public int EventProcessingHostedServiceTimeSpanInSeconds { get; set; }
        public int EventProcessingHostedServiceTotalItemsProcessedPerTimes { get; set; }
        public int RefundProcessingHostedServiceTimeSpanInHour { get; set; }
        public int RefundProcessingHostedServiceTimeSpanDelayInHour { get; set; }
        public int RefundProcessingHostedServiceMaxRetries { get; set; }
        public bool IsRefundAfterOneDay { get; set; }
        public bool IsReCheckoutOrder { get; set; }
        public bool IsProduction { get; set; }
    }
}
