using System;
using System.Text.RegularExpressions;

namespace DXP.SmartConnectPickup.Common.Utils
{
    public static class Formats
    {
        public static string GetSearchPhoneNumber(string phoneNumber)
        {
            if (!string.IsNullOrEmpty(phoneNumber))
            {
                return Regex.Replace(phoneNumber, @"[^\d]", "");
            }

            return phoneNumber;
        }

        public static Guid ToGuid(string input)
        {
            _ = Guid.TryParse(input, out Guid result);
            return result;
        }
    }
}
