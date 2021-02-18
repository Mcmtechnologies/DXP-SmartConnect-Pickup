using System;

namespace DXP.SmartConnectPickup.Common.Models
{
    public class TransactionModel
    {
        public long TransactionId { get; set; }
        public string Description { get; set; }
        public string PaymentId { get; set; }
        public string UserId { get; set; }
        public string PaymentProvider { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string PaymentStatus { get; set; }
        public decimal? PaymentFee { get; set; }
        public decimal? PaymentAmount { get; set; }
        public long? OrderId { get; set; }
        public Guid? CorrelationId { get; set; }
        public string PaymentType { get; set; }
        public string PaymentCaptureMethod { get; set; }
        public string PaymentMethod { get; set; }
        public int? ServiceId { get; set; }
        public string ExternalTransactionId { get; set; }
        public string ExternalStatus { get; set; }
        public decimal? ExternalApprovedAmount { get; set; }
        public decimal? ExternalRequestAmount { get; set; }
        public string ExternalErrorCode { get; set; }
        public string ExternalResponseMessage { get; set; }
        public string ExternalCustomerId { get; set; }
        public string ExternalAccountId { get; set; }
        public string ExternalCorrelationId { get; set; }
        public long? OriginalTransactionId { get; set; }
        public string ExternalCurrencyCode { get; set; }
        public decimal? DepositAmount { get; set; }
        public bool? IsCancelled { get; set; }
        public bool? IsCaptured { get; set; }
        public decimal? RefundedAmount { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
