using DXP.SmartConnectPickup.Common.Models;
using System;

namespace DXP.SmartConnectPickup.DataServices.Models
{
    public partial class Setting : Entity<int>
    {
        public string SettingName { get; set; }
        public string SettingValue { get; set; }
        public int? DisplayOrder { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
