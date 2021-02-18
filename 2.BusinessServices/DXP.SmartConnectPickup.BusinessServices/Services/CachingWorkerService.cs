using DXP.SmartConnectPickup.BusinessServices.Interfaces;
using DXP.SmartConnectPickup.DataServices.Interfaces;
using DXP.SmartConnectPickup.DataServices.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;

namespace DXP.SmartConnectPickup.BusinessServices.Services
{
    public class CachingWorkerService : ICachingWorkerService
    {
        private readonly ILogger<CachingWorkerService> _logger;
        private readonly ISettingRepository _settingRepository;
        private readonly IMemoryCache _cache;
        private const string IsLoggingDatabaseCacheKey = "IsLoggingDatabaseCacheKey";
        private const string IsLoggingDatabaseSettingName = "IsLoggingDatabase";

        public CachingWorkerService(ILogger<CachingWorkerService> logger,
            ISettingRepository settingRepository,
            IMemoryCache cache)
        {
            _logger = logger;
            _settingRepository = settingRepository;
            _cache = cache;
        }

        /// <summary>
        /// Is logging database.
        /// </summary>
        /// <returns>System.Boolean.</returns>
        public bool IsLoggingDatabase()
        {
            try
            {
                if (_cache.TryGetValue(IsLoggingDatabaseCacheKey, out bool isLoggingDatabase))
                {
                    return isLoggingDatabase;
                }

                Setting setting = _settingRepository.GetByName(IsLoggingDatabaseSettingName);
                bool result = false;

                if (setting != null)
                {
                    result = bool.Parse(setting.SettingValue);
                }

                _cache.Set(IsLoggingDatabaseCacheKey, result, TimeSpan.FromMinutes(5));
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Call to {nameof(IsLoggingDatabase)} failed.");
                return false;
            }
        }
    }
}
