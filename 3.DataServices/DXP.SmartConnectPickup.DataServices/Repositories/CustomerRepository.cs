using DXP.SmartConnectPickup.Common.Repository;
using DXP.SmartConnectPickup.DataServices.Context;
using DXP.SmartConnectPickup.DataServices.Interfaces;
using DXP.SmartConnectPickup.DataServices.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DXP.SmartConnectPickup.DataServices.Repositories
{
    public class CustomerRepository : GenericRepository<DBContext, Customer, string>, ICustomerRepository
    {
        public CustomerRepository(DBContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Get Customer By User Id And Provider Async.
        /// </summary>
        /// <param name="userId">The userId.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>Task{Customer}.</returns>
        public async Task<Customer> GetCustomerByUserIdAndProviderAsync(string userId, string provider)
        {
            return await _dbContext.Customer.FirstOrDefaultAsync(x => x.UserId == userId && x.Provider == provider);
        }

        /// <summary>
        /// Get Customer By User Id Async.
        /// </summary>
        /// <param name="userId">The userId.</param>
        /// <returns>Task{Customer}.</returns>
        public async Task<Customer> GetCustomerByUserIdAsync(string userId)
        {
            return await _dbContext.Customer.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        /// <summary>
        /// GetCustomerNotSyncByProvider Async.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="pagesize">The pagesize.</param>
        /// <param name="index">The index.</param>
        /// <returns>Task{IEnumerable{Customer}}</returns>
        public async Task<IEnumerable<Customer>> GetCustomerNotSyncByProviderAsync(string provider, int pagesize = 10, int index = 1)
        {
            return await _dbContext.Customer.Where(x => x.IsSync == false && x.Provider == provider)
                .Skip((index - 1) * pagesize)
                .Take(pagesize)
                .ToArrayAsync();
        }
    }
}
