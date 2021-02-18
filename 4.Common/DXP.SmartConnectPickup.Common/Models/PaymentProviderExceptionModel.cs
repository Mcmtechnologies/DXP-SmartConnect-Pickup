using System;

namespace DXP.SmartConnectPickup.Common.Models
{
    public class PaymentProviderExceptionModel
    {
        public long PaymentProviderExceptionId { get; set; }
        public Guid? CorrelationId { get; set; }
        public long? TransactionId { get; set; }
        public string Type { get; set; }
        public long? OrderId { get; set; }
        public string UserId { get; set; }
        public string ExceptionMessage { get; set; }
        public string StoreId { get; set; }
        public string PaymentProvider { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
