namespace DXP.SmartConnectPickup.BusinessServices.PickupProcessing.Adapters
{
    public class BaseCustomerResponse : BasePickupResponse
    {
        public string UserId { get; set; }

        public string ExternalId { get; set; }

        public string ExternalApiToken { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string CarColor { get; set; }

        public string CarType { get; set; }

        public string LisensePlate { get; set; }
    }
}
