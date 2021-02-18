using System;

namespace DXP.SmartConnectPickup.Common.Models
{
    public class PaymentProviderSearchModel
    {
        public Guid? CorrelationId { get; set; }

        public string UserId { get; set; }

        public string OrderId { get; set; }

        public string TransactionId { get; set; }

        public string StoreId { get; set; }

        public string PaymentProvider { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public long? PageIndex { get; set; }

        public int? PageSize { get; set; }
    }
}
