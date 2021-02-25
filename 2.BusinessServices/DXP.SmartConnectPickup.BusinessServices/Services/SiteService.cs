using DXP.SmartConnectPickup.BusinessServices.Interfaces;
using DXP.SmartConnectPickup.BusinessServices.Models;
using DXP.SmartConnectPickup.Common.ApplicationSettings;
using DXP.SmartConnectPickup.Common.Constants;
using DXP.SmartConnectPickup.Common.Models;
using DXP.SmartConnectPickup.Common.Services;
using DXP.SmartConnectPickup.Common.Utils;
using DXP.SmartConnectPickup.DataServices.Interfaces;
using DXP.SmartConnectPickup.DataServices.Models;
using MapsterMapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace DXP.SmartConnectPickup.BusinessServices.Services
{
    public class SiteService : GenericService, ISiteService
    {
        private readonly ISiteRepository _siteRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationSettings _applicationSettings;
        private readonly MerchantAccountSettings _merchantAccountSettings;
        public SiteService(ISiteRepository siteRepository,
            IMapper mapper,
            IOptions<ApplicationSettings> applicationSettings,
            IOptions<MerchantAccountSettings> merchantAccountSettings)
        {
            _siteRepository = siteRepository;
            _mapper = mapper;
            _applicationSettings = applicationSettings.Value;
            _merchantAccountSettings = merchantAccountSettings.Value;
        }

        public async Task<BaseResponseObject> CreateSiteAsync(SiteModel model)
        {
            Guard.AgainstNullOrEmpty(nameof(model.ExternalSiteId), model.ExternalSiteId);
            Guard.AgainstNullOrEmpty(nameof(model.StoreApiId), model.StoreApiId);

            Site siteExists = await _siteRepository.GetSiteByStoreIdAndProvider(model.StoreApiId, _merchantAccountSettings.PickupProviderDefault);

            Guard.AgainstInvalidArgument("Site is already Exists!", siteExists == null);

            var site = new Site()
            {
                Id = Guid.NewGuid().ToString(),
                StoreId = model.StoreApiId,
                StoreCode = model.StoreCode,
                ExternalId = model.ExternalSiteId,
                Provider = _merchantAccountSettings.PickupProviderDefault,
                CreatedBy = AuthorizationConstants.SITE_ADMIN_ROLE,
                CreatedDate = DateTime.UtcNow,
                ModifiedBy = AuthorizationConstants.SITE_ADMIN_ROLE,
                ModifiedDate = DateTime.UtcNow,
            };

            await _siteRepository.AddAndSaveChangesAsync(site);

            return ReturnSuccess(site);
        }

        public async Task<BaseResponseObject> UpdateSiteAsync(SiteModel model)
        {
            Guard.AgainstNullOrEmpty(nameof(model.ExternalSiteId), model.ExternalSiteId);
            Guard.AgainstNullOrEmpty(nameof(model.StoreApiId), model.StoreApiId);

            Site site = await _siteRepository.GetSiteByStoreIdAndProvider(model.StoreApiId, _merchantAccountSettings.PickupProviderDefault);
            Guard.AgainstInvalidArgument("Site is not found!", site != null);

            site.StoreCode = model.StoreCode ?? site.StoreCode;
            site.ExternalId = model.ExternalSiteId;
            site.ModifiedBy = AuthorizationConstants.SITE_ADMIN_ROLE;
            site.ModifiedDate = DateTime.UtcNow;

            await _siteRepository.UpdateAndSaveChangesAsync(site);

            return ReturnSuccess(site);
        }

        public async Task<BaseResponseObject> GetSiteByStoreApiId(string storeApiId)
        {
            return ReturnSuccess(await _siteRepository.GetSiteByStoreIdAndProvider(storeApiId, _merchantAccountSettings.PickupProviderDefault));
        }

       
    }
}
