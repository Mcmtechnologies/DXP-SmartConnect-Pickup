namespace DXP.SmartConnectPickup.Common.ApplicationSettings
{
    public class FaultHandlingSettings
    {
        public int MaxRetryAttempts { get; set; }
        public int InitialRetryDelayInMs { get; set; }
        public int DurationOnBreakInSec { get; set; }
        public int ExceptionsAllowedBeforeBreaking { get; set; }
        public int WebApiTimeoutInMs { get; set; }
    }
}
