using DXP.SmartConnectPickup.Common.Repository;
using DXP.SmartConnectPickup.DataServices.Context;
using DXP.SmartConnectPickup.DataServices.Interfaces;
using DXP.SmartConnectPickup.DataServices.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DXP.SmartConnectPickup.DataServices.Repositories
{
    public class StoreServiceRepository : GenericRepository<DBContext, StoreService, string>, IStoreServiceRepository
    {
        public StoreServiceRepository(DBContext dbContext) : base(dbContext)
        {
        }
    }
}
