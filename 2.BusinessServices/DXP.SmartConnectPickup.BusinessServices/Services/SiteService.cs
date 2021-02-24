using DXP.SmartConnectPickup.BusinessServices.Interfaces;
using DXP.SmartConnectPickup.DataServices.Interfaces;

namespace DXP.SmartConnectPickup.BusinessServices.Services
{
    public class SiteService : ISiteService
    {
        private readonly ISiteRepository _siteRepository;

        public SiteService(ISiteRepository siteRepository)
        {
            _siteRepository = siteRepository;
        }
    }
}
