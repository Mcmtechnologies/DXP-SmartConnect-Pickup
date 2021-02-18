namespace DXP.SmartConnectPickup.Common.Enums
{
    public enum OrderPaymentStatus
    {
        Unknown = 0,
        NotPaid = 1,
        PartialPaid = 2,
        Paid = 3,
        PartialRefund = 4,
        FullRefund = 5,
        RefundProcessing = 6,
        Failed = 7
    }
}
