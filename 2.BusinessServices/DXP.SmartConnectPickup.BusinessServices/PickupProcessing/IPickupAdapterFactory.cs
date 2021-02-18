using DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters;
using DXP.SmartConnectPickup.Common.Models;

namespace DXP.SmartConnectPickup.BusinessServices.PickupProcessing
{
    public interface IPickupAdapterFactory
    {
        IPickupTarget BuildPickupAdapter(MerchantAccount mechantAccount);
    }
}
