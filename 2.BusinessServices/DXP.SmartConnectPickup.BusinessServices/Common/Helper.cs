namespace DXP.SmartConnectPickup.BusinessServices.Common
{
    public static class Helpers
    {
        public static bool IsProduction { get; set; }

        public static string GetDeveloperInfo(string input)
        {
            return IsProduction ? string.Empty : input;
        }
    }
}
