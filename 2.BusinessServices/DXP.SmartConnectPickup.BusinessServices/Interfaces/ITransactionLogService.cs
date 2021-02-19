using DXP.SmartConnectPickup.Common.Enums;
using DXP.SmartConnectPickup.Common.Models;
using DXP.SmartConnectPickup.DataServices.Models;
using System;
using System.Threading.Tasks;

namespace DXP.SmartConnectPickup.BusinessServices.Interfaces
{
    public interface ITransactionLogService
    {
        Task<TransactionLog> AddTransactionLogAsync(TransactionLog transactionLog);

        Task<TransactionLog> AddTransactionLogAsync(TransactionLogStep transactionLogStep, TransactionLogStatus transactionLogStatus, string requestData, string responseData, string exceptionMessage, string exception, Guid correlationId);

        Task AddTransactionLogExceptionAsync(TransactionLogStep transactionLogStep, BaseRequestObject request, Exception exception);
    }
}

