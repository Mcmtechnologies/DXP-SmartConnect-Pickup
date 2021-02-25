using DXP.SmartConnectPickup.Common.Models;
using System;

namespace DXP.SmartConnectPickup.DataServices.Models
{
    public class Site : Entity<string>
    {
        public string Provider { get; set; }
        public string ExternalId { get; set; }
        public string StoreId { get; set; }
        public string StoreCode { get; set; }
        public string JsonObject { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
