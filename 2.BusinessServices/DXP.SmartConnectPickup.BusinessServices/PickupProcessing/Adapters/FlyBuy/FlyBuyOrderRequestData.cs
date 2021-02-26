using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters.FlyBuy
{
    public class FlyBuyOrderRequestData
    {
        [JsonProperty(PropertyName = "site_id")]
        public string SiteId { get; set; }

        [JsonProperty(PropertyName = "customer_phone")]
        public string CustomerPhone { get; set; }

        [JsonProperty(PropertyName = "customer_name")]
        public string CustomerName { get; set; }

        [JsonProperty(PropertyName = "customer_token")]
        public string CustomerToken { get; set; }

        [JsonProperty(PropertyName = "app_authorization_token")]
        public string AppAuthorizationToken { get; set; }

        [JsonProperty(PropertyName = "customer_car_color")]
        public string CustomerCarColor { get; set; }

        [JsonProperty(PropertyName = "customer_car_type")]
        public string CustomerCarType { get; set; }

        [JsonProperty(PropertyName = "customer_license_plate")]
        public string CustomerLicensePlate { get; set; }

        [JsonProperty(PropertyName = "partner_identifier")]
        public string PartnerIdentifier { get; set; }

        [JsonProperty(PropertyName = "partner_identifier_for_crew")]
        public string PartnerIdentifierForCrew { get; set; }

        [JsonProperty(PropertyName = "partner_identifier_for_customer")]
        public string PartnerIdentifierForCustomer { get; set; }

        [JsonProperty(PropertyName = "push_token")]
        public string PushToken { get; set; }

        [JsonProperty(PropertyName = "pickup_window")]
        public string PickupWindow { get; set; }

        [JsonProperty(PropertyName = "pickup_type")]
        public string PickupType { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }
    }
}
