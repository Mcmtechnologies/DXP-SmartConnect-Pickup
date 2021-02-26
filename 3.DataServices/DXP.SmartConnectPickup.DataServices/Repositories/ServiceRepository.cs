using DXP.SmartConnectPickup.Common.Repository;
using DXP.SmartConnectPickup.DataServices.Context;
using DXP.SmartConnectPickup.DataServices.Interfaces;
using DXP.SmartConnectPickup.DataServices.Models;

namespace DXP.SmartConnectPickup.DataServices.Repositories
{
    public class ServiceRepository : GenericRepository<DBContext, Service, string>, IServiceRepository
    {
        public ServiceRepository(DBContext dbContext) : base(dbContext)
        {
        }
    }
}
