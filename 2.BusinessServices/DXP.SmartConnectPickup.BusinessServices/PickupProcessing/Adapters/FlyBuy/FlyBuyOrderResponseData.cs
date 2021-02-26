using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters.FlyBuy
{
    public class FlyBuyOrderResponseData
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "order_id")]
        public string OrderId { get; set; }

        [JsonProperty(PropertyName = "order_state")]
        public string OrderState { get; set; }

        [JsonProperty(PropertyName = "redemption_url")]
        public string RedemptionUrl { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "arrived_at")]
        public string ArrivedAt { get; set; }

        [JsonProperty(PropertyName = "customer_state")]
        public string CustomerState { get; set; }

        [JsonProperty(PropertyName = "eta_at")]
        public string EtaAt { get; set; }

        [JsonProperty(PropertyName = "partner_identifier")]
        public string PartnerIdentifier { get; set; }

        [JsonProperty(PropertyName = "partner_display_identifier")]
        public string PartnerDisplayIdentifier { get; set; }

        [JsonProperty(PropertyName = "partner_identifier_for_crew")]
        public string PartnerIdentifierForCrew { get; set; }

        [JsonProperty(PropertyName = "partner_identifier_for_customer")]
        public string PartnerIdentifierForCustomer { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "redemption_code")]
        public string RedemptionCode { get; set; }

        [JsonProperty(PropertyName = "customer_id")]
        public string CustomerId { get; set; }

        [JsonProperty(PropertyName = "site_id")]
        public string SiteId { get; set; }

        [JsonProperty(PropertyName = "site_partner_identifier")]
        public string SitePartnerIdentifier { get; set; }

        [JsonProperty(PropertyName = "customer_name")]
        public string CustomerName { get; set; }

        [JsonProperty(PropertyName = "customer_car_color")]
        public string CustomerCarColor { get; set; }

        [JsonProperty(PropertyName = "customer_car_type")]
        public string CustomerCarType { get; set; }

        [JsonProperty(PropertyName = "customer_license_plate")]
        public string CustomerLicensePlate { get; set; }

        [JsonProperty(PropertyName = "customer_rating_value")]
        public string CustomerRatingValue { get; set; }

        [JsonProperty(PropertyName = "customer_rating_value_string")]
        public string CustomerRatingValueString { get; set; }

        [JsonProperty(PropertyName = "customer_rating_comments")]
        public string CustomerRatingComments { get; set; }

        [JsonProperty(PropertyName = "pickup_window")]
        public string PickupWindow { get; set; }

        [JsonProperty(PropertyName = "pickup_type")]
        public string PickupType { get; set; }

        [JsonProperty(PropertyName = "push_token")]
        public string PushToken { get; set; }
    }
}
