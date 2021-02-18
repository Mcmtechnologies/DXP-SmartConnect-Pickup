using DXP.SmartConnectPickup.Common.Repository;
using DXP.SmartConnectPickup.DataServices.Context;
using DXP.SmartConnectPickup.DataServices.Interfaces;
using DXP.SmartConnectPickup.DataServices.Models;

namespace DXP.SmartConnectPickup.DataServices.Repositories
{
    public class TransactionLogRepository : GenericRepository<DBContext, TransactionLog, long>, ITransactionLogRepository
    {
        public TransactionLogRepository(DBContext dbContext) : base(dbContext)
        {
        }
    }
}
