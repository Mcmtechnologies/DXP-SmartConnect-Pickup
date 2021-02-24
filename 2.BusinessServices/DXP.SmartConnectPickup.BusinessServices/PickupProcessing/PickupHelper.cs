using DXP.SmartConnectPickup.BusinessServices.Interfaces;
using DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters;
using DXP.SmartConnectPickup.Common.Enums;
using DXP.SmartConnectPickup.Common.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace DXP.SmartConnectPickup.BusinessServices.PickupProcessing
{
    public static class PickupHelper
    {
        /// <summary>
        /// Build Pickup Adapter.
        /// </summary>
        /// <param name="pickupProvider">The pickupProvider.</param>
        /// <returns>IPickupTarget.</returns>
        public static IPickupTarget BuildPickupAdapter(IPickupAdapterFactory _pickupAdapterFactory, string pickupProvider)
        {
            var mechantAccount = new MerchantAccount();
            mechantAccount.SetMerchantAccountType(pickupProvider);
            IPickupTarget pickupTarget = _pickupAdapterFactory.BuildPickupAdapter(mechantAccount);
            return pickupTarget;
        }

        public static async Task HandleErrorResponse(ITransactionLogService _transactionLogService, BaseRequestObject request, BasePickupResponse response, TransactionLogStep transactionLogStep)
        {
            if (response.RequestError != null || response.Errors != null || response.Error != null)
            {
                string responseError = response.Errors != null ? JsonConvert.SerializeObject(response.Errors) : null;
                responseError ??= (response.Error != null ? JsonConvert.SerializeObject(response.Error) : "");

                string requestData = response.RequestData != null ? JsonConvert.SerializeObject(response.RequestData) : "";
                string exception = response.RequestError != null ? JsonConvert.SerializeObject(response.RequestError) : "";

                await _transactionLogService.AddTransactionLogAsync(transactionLogStep, TransactionLogStatus.Error, requestData, responseError, response.RequestError?.Message, exception, request.GetCorrelationId());
            }
        }
    }
}
