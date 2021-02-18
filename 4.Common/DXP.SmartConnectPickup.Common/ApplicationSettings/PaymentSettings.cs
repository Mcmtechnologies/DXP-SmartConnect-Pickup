namespace DXP.SmartConnectPickup.Common.ApplicationSettings
{
    public class PaymentSettings
    {
        public bool IsAllowRefundingDeposit { get; set; }

        public bool IsAuthorizeTotalAmountExceptDeposit { get; set; }

        public bool IsAllowPartialPayment { get; set; }

        public bool IsAutoCompleteOrder { get; set; }

        public bool IsLogFailedTransaction { get; set; }
    }
}
