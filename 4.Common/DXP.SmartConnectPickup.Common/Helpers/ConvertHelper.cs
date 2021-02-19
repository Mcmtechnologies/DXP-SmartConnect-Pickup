using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace DXP.SmartConnectPickup.Common.Helpers
{
    public static class ConvertHelper
    {
        private const string HTML_TAG_PATTERN = "<.*?>";

        private static object ParseFromString(string input, Type typeToParse)
        {
            var converter = TypeDescriptor.GetConverter(typeToParse);
            if (converter != null)
            {
                return converter.ConvertFromString(input);
            }
            return null;
        }

        public static T ToSingle<T>(this Dictionary<string, string> keypairs)
            where T : new()
        {
            var t = new T();

            var properties = typeof(T).GetProperties();

            foreach (var tag in keypairs)
            {
                var property = properties.SingleOrDefault(p => p.Name == tag.Key);

                if (property != null)
                {
                    Type pType = property.PropertyType;
                    property.SetValue(t, ParseFromString(tag.Value, pType), null);
                }
            }

            return t;
        }

        public static long ToLong(string sId)
        {
            long.TryParse(sId, out long result);
            return result;
        }

        public static int ToInt(string sId)
        {
            int.TryParse(sId, out int result);
            return result;
        }

        public static short ToShort(string sId)
        {
            short.TryParse(sId, out short result);
            return result;
        }

        public static short ToInt16(string bin)
        {
            long l = Convert.ToInt64(bin, 2);
            short i = (short)l;
            return i;
        }
        public static DateTime ToDateTime(string sId)
        {
            DateTime.TryParse(sId, out DateTime result);
            return result;
        }

        public static DateTime ToDateTime(string sid, string format)
        {
            return DateTime.ParseExact(sid, format, System.Globalization.CultureInfo.InvariantCulture);
        }

        public static bool ToBool(string sId)
        {
            bool.TryParse(sId, out bool result);
            return result;
        }

        public static decimal ToDecimal(string sId)
        {
            decimal.TryParse(sId, out decimal result);
            return result;
        }

        public static double ToDouble(string sId)
        {
            double.TryParse(sId, out double result);
            return result;
        }

        public static Guid ToGuid(string sId)
        {
            Guid.TryParse(sId, out Guid result);
            return result;
        }

        public static string TrimAll(string input)
        {
            return input.Replace(" ", "");
        }

        /// <summary>
        /// Convert 1234.5678 to $1.234.56
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>System.String.</returns>
        public static string ToMoneyText(decimal input)
        {
            return input.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-us"));
        }

        /// <summary>
        /// Convert 1234.5678 to 1.234.56
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>System.String.</returns>
        public static decimal ToMoneyValue(decimal input)
        {
            var moneyValue = string.Format("{0:N}", input);
            return ToDecimal(moneyValue);
        }

        public static string ToBinary(short Decimal, int n = 3)
        {
            string binary = Convert.ToString((long)Decimal, 2);
            int len = binary.Length;
            if (n > len)
            {
                for (int i = 0; i < n - len; i++)
                    binary = "0" + binary;
            }
            return binary;
        }

        public static string RemoveSpecialChars(string input)
        {
            if (!string.IsNullOrEmpty(input))
                return Regex.Replace(input, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
            return input;
        }

        public static string RemoveHTMLTag(string value)
        {
            var returnValue = Regex.Replace(value, HTML_TAG_PATTERN, string.Empty)
                .Replace("\n", string.Empty)
                .Replace("&nbsp;", string.Empty).Trim();
            return returnValue;
        }
    }
}
