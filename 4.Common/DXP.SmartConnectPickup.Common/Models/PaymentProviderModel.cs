using System.Collections.Generic;

namespace DXP.SmartConnectPickup.Common.Models
{
    public class PaymentProviderModel
    {
        public IList<TransactionModel> TransactionViewModels { get; set; }

        public IList<PaymentProviderOutboundModel> PaymentProviderOutboundViewModels { get; set; }

        public IList<PaymentProviderExceptionModel> PaymentProviderExceptionViewModels { get; set; }
    }
}
