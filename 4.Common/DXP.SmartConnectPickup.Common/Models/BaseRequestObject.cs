using System;

namespace DXP.SmartConnectPickup.Common.Models
{
    public class BaseRequestObject
    {
        public Guid? CorrelationId { get; set; }

        /// <summary>
        /// Resets correlation id from client side.
        /// </summary>
        public void ResetCorrelationId()
        {
            CorrelationId = Guid.NewGuid();
        }

        public Guid GetCorrelationId()
        {
            if (CorrelationId == null || CorrelationId == Guid.Empty)
            {
                CorrelationId = Guid.NewGuid();
            }

            return CorrelationId.Value;
        }
    }
}
