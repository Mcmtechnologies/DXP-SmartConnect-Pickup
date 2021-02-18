using DXP.SmartConnectPickup.Common.Constants;
using DXP.SmartConnectPickup.Common.Enums;
using System;
using System.Collections.Generic;

namespace DXP.SmartConnectPickup.Common.Utils
{
    public static class PaymentUtils
    {
        private static Dictionary<string, PaymentStatus> _paymentStatusMappings = new Dictionary<string, PaymentStatus>()
        {
            {
                StripePaymentStatusConstants.Succeeded,
                PaymentStatus.Succeeded
            },
            {
                StripePaymentStatusConstants.RequiresCapture,
                PaymentStatus.RequiresCapture
            },
            {
                StripePaymentStatusConstants.Processing,
                PaymentStatus.Processing
            },
            {
                StripePaymentStatusConstants.Cancelled,
                PaymentStatus.Cancelled
            },
            {
                FirstDataUCommPaymentStatusConstants.Approved,
                PaymentStatus.Succeeded
            },
            {
                FirstDataUCommPaymentStatusConstants.Cancelled,
                PaymentStatus.Cancelled
            }
        };

        public static string GetInternalPaymentStatus(string paymentStatus)
        {
            if (!string.IsNullOrEmpty(paymentStatus))
            {
                foreach (var element in _paymentStatusMappings)
                {
                    if (paymentStatus.Equals(element.Key, StringComparison.OrdinalIgnoreCase))
                    {
                        return element.Value.ToString();
                    }
                }
            }
            return paymentStatus;
        }

        public static string GetOrderPaymentMethod(string onlinePaymentType)
        {
            if (onlinePaymentType == OnlinePaymentType.DepositOnline.ToString())
            {
                return PaymentMethodConstants.PayDeposit;
            }
            else if (onlinePaymentType == OnlinePaymentType.PayFullOnline.ToString())
            {
                return PaymentMethodConstants.PayOnline;
            }

            return PaymentMethodConstants.PayInStore;
        }
    }
}
