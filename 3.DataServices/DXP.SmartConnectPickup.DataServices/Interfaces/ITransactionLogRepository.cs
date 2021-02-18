using DXP.SmartConnectPickup.Common.Repository;
using DXP.SmartConnectPickup.DataServices.Models;

namespace DXP.SmartConnectPickup.DataServices.Interfaces
{
    public interface ITransactionLogRepository : IGenericRepository<TransactionLog, long>
    {
    }
}