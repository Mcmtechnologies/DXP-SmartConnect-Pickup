using DXP.SmartConnectPickup.BusinessServices.Interfaces;
using DXP.SmartConnectPickup.Common.Constants;
using DXP.SmartConnectPickup.Common.Enums;
using DXP.SmartConnectPickup.Common.Models;
using DXP.SmartConnectPickup.DataServices.Interfaces;
using DXP.SmartConnectPickup.DataServices.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace DXP.SmartConnectPickup.BusinessServices.Services
{
    public class TransactionLogService : ITransactionLogService
    {
        private readonly ILogger<TransactionLogService> _logger;
        private readonly ITransactionLogRepository _transactionLogRepository;

        public TransactionLogService(ILogger<TransactionLogService> logger,
            ITransactionLogRepository transactionLogRepository)
        {
            _logger = logger;
            _transactionLogRepository = transactionLogRepository;
        }

        /// <summary>
        /// Adds transaction log.
        /// </summary>
        /// <param name="transactionLog">The transactionLog.</param>
        /// <returns>TransactionLog.</returns>
        public async Task<TransactionLog> AddTransactionLogAsync(TransactionLog transactionLog)
        {
            try
            {
                if (transactionLog.CorrelationId == Guid.Empty || transactionLog.CorrelationId == null)
                {
                    transactionLog.CorrelationId = Guid.NewGuid();
                }

                transactionLog.HostName = Dns.GetHostName();
                transactionLog.CreatedBy = AuthorizationConstants.SITE_ADMIN_ROLE;
                transactionLog.CreatedDate = DateTime.UtcNow;

                _logger.LogInformation($"Call to {nameof(AddTransactionLogAsync)} with LogData: {JsonConvert.SerializeObject(transactionLog)}.");

                await _transactionLogRepository.AddAndSaveChangesAsync(transactionLog);

                return transactionLog;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Call to {nameof(AddTransactionLogAsync)} failed.");
                return transactionLog;
            }
        }

        /// <summary>
        /// Adds transaction log.
        /// </summary>
        /// <param name="transactionLogStep">The transactionLogStep.</param>
        /// <param name="transactionLogStatus">The transactionLogStatus.</param>
        /// <param name="requestData">The requestData.</param>
        /// <param name="responseData">The responseData.</param>
        /// <param name="exceptionMessage">The exceptionMessage.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="correlationId">The correlationId.</param>
        /// <returns>TransactionLog.</returns>
        public async Task<TransactionLog> AddTransactionLogAsync(TransactionLogStep transactionLogStep,
            TransactionLogStatus transactionLogStatus,
            string requestData,
            string responseData,
            string exceptionMessage,
            string exception,
            Guid correlationId
            )
        {
            TransactionLog transactionLog = new TransactionLog()
            {
                CorrelationId = correlationId,
                TransactionName = transactionLogStep.ToString(),
                Status = transactionLogStatus.ToString(),
                RequestData = requestData,
                ResponseData = responseData,
                ExceptionMessage = exceptionMessage,
                Exception = exception
            };

            return await AddTransactionLogAsync(transactionLog);
        }

        /// <summary>
        /// Adds transaction Exception log.
        /// </summary>
        /// <param name="transactionLogStep">The transactionLogStep.</param>
        /// <param name="requestData">The requestData.</param>
        /// <param name="exception">The exception.</param>
        /// <returns>Task.</returns>
        public async Task AddTransactionLogExceptionAsync(TransactionLogStep transactionLogStep, BaseRequestObject request, Exception exception)
        {
            try
            {
                TransactionLog transactionLog = new TransactionLog()
                {
                    CorrelationId = request?.GetCorrelationId(),
                    TransactionName = transactionLogStep.ToString(),
                    Status = TransactionLogStatus.Error.ToString(),
                    RequestData = request != null ? JsonConvert.SerializeObject(request) : null,
                    ExceptionMessage = exception.Message,
                    Exception = JsonConvert.SerializeObject(exception)
                };
                await AddTransactionLogAsync(transactionLog);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Call to {nameof(AddTransactionLogExceptionAsync)} failed.");
            }
        }
    }
}
