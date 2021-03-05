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

        // More Infor

        public string CustomerPhone { get; set; }

        public string CustomerName { get; set; }

        public string CustomerToken { get; set; }

        public string AppAuthorizationToken { get; set; }

        public string CustomerCarColor { get; set; }

        public string CustomerCarType { get; set; }

        public string CustomerLicensePlate { get; set; }

        public string PushToken { get; set; }
    }
}
