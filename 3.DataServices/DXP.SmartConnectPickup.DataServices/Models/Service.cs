using DXP.SmartConnectPickup.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DXP.SmartConnectPickup.DataServices.Models
{
    public class Service : Entity<string>
    {
        public string ServiceName { get; set; }

        public string ServiceShortName { get; set; }
    }
}
