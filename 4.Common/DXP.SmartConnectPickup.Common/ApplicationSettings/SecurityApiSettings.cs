namespace DXP.SmartConnectPickup.Common.ApplicationSettings
{
    public class SecurityApiSettings
    {
        public string SecurityAPIUrl { get; set; }
        public string ConsumerCode { get; set; }
        public Authorize Authorize { get; set; }
    }

    public class Authorize
    {
        public string Id { get; set; }

        public string ConsumerId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}