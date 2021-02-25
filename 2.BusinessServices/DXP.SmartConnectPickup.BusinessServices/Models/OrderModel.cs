using System;
using System.Collections.Generic;
using System.Text;

namespace DXP.SmartConnectPickup.BusinessServices.Models
{
    public class OrderModel
    {
        public string OrderApiId { get; set; }

        public string OrderNumber { get; set; }

        public string ServiceId { get; set; }

        public string StoreId { get; set; }

        public string StoreCode { get; set; }

        public string OrderStatus { get; set; }

        public string PickupWindow { get; set; }

        public string PickupType { get; set; }
    }
}
