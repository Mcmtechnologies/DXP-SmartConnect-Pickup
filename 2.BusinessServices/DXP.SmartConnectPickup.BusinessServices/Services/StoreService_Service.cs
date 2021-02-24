using DXP.SmartConnectPickup.BusinessServices.Interfaces;
using DXP.SmartConnectPickup.DataServices.Interfaces;
using DXP.SmartConnectPickup.DataServices.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXP.SmartConnectPickup.BusinessServices.Services
{
    public class StoreService_Service : IStoreService_Service
    {
        private readonly IStoreServiceRepository _storeServiceRepository;
        private readonly IMemoryCache _memoryCache;
        private const string keyCache = "StoreService_996B067C";
        public StoreService_Service(IStoreServiceRepository storeServiceRepository,
            IMemoryCache memoryCache)
        {
            _storeServiceRepository = storeServiceRepository;
            _memoryCache = memoryCache;
        }

        public async Task<List<StoreService>> GetAllStoreServices()
        {
            string key = keyCache + "_GetAll";
            _memoryCache.TryGetValue(keyCache, out List<StoreService> services);

            if (services == null)
            {
                services = await _storeServiceRepository.GetAll().ToListAsync();
                _memoryCache.Set(key, services);
            }

            return services;
        }

        public async Task<StoreService> GetStoreServicesById(string id)
        {
            string key = keyCache + "_Id: " + id;
            _memoryCache.TryGetValue(key, out StoreService service);

            if(service == null)
            {
                service = await _storeServiceRepository.GetByIdAsync(id);
                _memoryCache.Set(key, service);
            }

            return service;
        }
    }
}
