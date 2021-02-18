using DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters;
using DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters.FlyBuy;
using DXP.SmartConnectPickup.Common.Enums;
using DXP.SmartConnectPickup.Common.Models;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DXP.SmartConnectPickup.BusinessServices.PickupProcessing
{
    public class PickupAdapterFactory : IPickupAdapterFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public PickupAdapterFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Build Pickup Adapter.
        /// </summary>
        /// <param name="mechantAccount">The merchantAccount</param>
        /// <returns>IPickupTarget.</returns>
        public IPickupTarget BuildPickupAdapter(MerchantAccount mechantAccount)
        {
            if (mechantAccount.MerchantAccountType == MerchantAccountType.FlyBuy)
            {
                return _serviceProvider.GetService<IFlyBuyApiAdapter>();
            }

            throw new InvalidOperationException($"Invalid MerchantAccount: {mechantAccount.MerchantAccountType}");
        }
    }
}
