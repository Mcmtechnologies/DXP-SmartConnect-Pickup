using System;

namespace DXP.SmartConnectPickup.Common.Models
{
    public class PaymentProviderOutboundModel
    {
        public long PaymentProviderOutboundId { get; set; }
        public Guid? CorrelationId { get; set; }
        public long? TransactionId { get; set; }
        public string Type { get; set; }
        public long? OrderId { get; set; }
        public string UserId { get; set; }
        public string RequestHeader { get; set; }
        public string RequestURL { get; set; }
        public string RequestData { get; set; }
        public string ResponseData { get; set; }
        public string ResponseErrorCode { get; set; }
        public string ResponseErrorMessage { get; set; }
        public string PaymentStatus { get; set; }
        public string AdditionalData { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string StoreId { get; set; }
        public string PaymentProvider { get; set; }
    }
}
