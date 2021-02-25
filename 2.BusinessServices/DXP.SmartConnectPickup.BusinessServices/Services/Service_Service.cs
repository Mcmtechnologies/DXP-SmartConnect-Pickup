using DXP.SmartConnectPickup.BusinessServices.Interfaces;
using DXP.SmartConnectPickup.DataServices.Interfaces;
using DXP.SmartConnectPickup.DataServices.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DXP.SmartConnectPickup.BusinessServices.Services
{
    public class Service_Service : IService_Service
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMemoryCache _memoryCache;
        private const string keyCache = "StoreService_996B067C";
        public Service_Service(IServiceRepository serviceRepository,
            IMemoryCache memoryCache)
        {
            _serviceRepository = serviceRepository;
            _memoryCache = memoryCache;
        }

        public async Task<List<Service>> GetAllStoreServices()
        {
            string key = keyCache + "_GetAll";
            _memoryCache.TryGetValue(keyCache, out List<Service> services);

            if (services == null)
            {
                services = await _serviceRepository.GetAll().ToListAsync();
                _memoryCache.Set(key, services);
            }

            return services;
        }

        public async Task<Service> GetStoreServicesById(string id)
        {
            string key = keyCache + "_Id: " + id;
            _memoryCache.TryGetValue(key, out Service service);

            if (service == null)
            {
                service = await _serviceRepository.GetByIdAsync(id);
                _memoryCache.Set(key, service);
            }

            return service;
        }
    }
}
