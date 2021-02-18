using DXP.SmartConnectPickup.Common.Constants;
using DXP.SmartConnectPickup.Common.Enums;
using System;

namespace DXP.SmartConnectPickup.Common.Models
{
    public class MerchantAccount
    {
        public MerchantAccountType MerchantAccountType { get; set; }

        public void SetMerchantAccountType(string paymentProvider)
        {
            MerchantAccountType = MerchantAccountType.Unknown;

            if (paymentProvider.Equals(PickupProviderConstants.FlyBuy, StringComparison.OrdinalIgnoreCase))
            {
                MerchantAccountType = MerchantAccountType.FlyBuy;
            }
        }
    }
}
