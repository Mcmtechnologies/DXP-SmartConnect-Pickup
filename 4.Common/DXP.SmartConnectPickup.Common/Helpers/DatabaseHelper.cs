using System;

namespace DXP.SmartConnectPickup.Common.Helpers
{
    public static class DatabaseHelper
    {
        public static string GetString(object value)
        {
            string stringValue = string.Empty;
            if (value != null && !Convert.IsDBNull(value))
            {
                stringValue = value.ToString().Trim();
            }

            return stringValue;
        }

        public static string GetNullableString(object value)
        {
            string result = string.Empty;
            if (!Convert.IsDBNull(value))
            {
                result = value.ToString();
            }

            return result;
        }

        public static int GetInteger(object value)
        {
            int integerValue = 0;
            if (value != null && !Convert.IsDBNull(value))
            {
                int.TryParse(value.ToString(), out integerValue);
            }

            return integerValue;
        }

        public static int? GetNullableInteger(object value)
        {
            int? nullableIntegerValue = null;
            if (value != null && !Convert.IsDBNull(value) && int.TryParse(value.ToString(), out int integerValue))
            {
                nullableIntegerValue = integerValue;
            }

            return nullableIntegerValue;
        }

        public static long GetLong(object value)
        {
            long longValue = 0;
            if (value != null && !Convert.IsDBNull(value))
            {
                long.TryParse(value.ToString(), out longValue);
            }

            return longValue;
        }

        public static long? GetNullableLong(object value)
        {
            int? nullableLongValue = null;
            if (value != null && !Convert.IsDBNull(value) && int.TryParse(value.ToString(), out int longValue))
            {
                nullableLongValue = longValue;
            }

            return nullableLongValue;
        }

        public static decimal? GetNullableDecimal(object value)
        {
            int? nullableDecimalValue = null;
            if (value != null && !Convert.IsDBNull(value) && int.TryParse(value.ToString(), out int decimalValue))
            {
                nullableDecimalValue = decimalValue;
            }

            return nullableDecimalValue;
        }

        public static bool GetBoolean(object value)
        {
            bool booleanValue = false;
            if (value != null && !Convert.IsDBNull(value))
            {
                bool.TryParse(value.ToString(), out booleanValue);
            }

            return booleanValue;
        }

        public static short GetShort(object value)
        {
            short shortValue = 0;
            if (value != null && !Convert.IsDBNull(value))
            {
                short.TryParse(value.ToString(), out shortValue);
            }

            return shortValue;
        }

        public static short? GetNullableShort(object value)
        {
            short? nullableShortValue = null;
            if (value != null && !Convert.IsDBNull(value) && short.TryParse(value.ToString(), out short shortValue))
            {
                nullableShortValue = shortValue;
            }

            return nullableShortValue;
        }

        public static DateTime? GetNullableDateTime(object value)
        {
            DateTime? nullableDateTimeValue = null;
            if (value != null && !Convert.IsDBNull(value) && DateTime.TryParse(value.ToString(), out DateTime dateTimeValue))
            {
                nullableDateTimeValue = dateTimeValue;
            }

            return nullableDateTimeValue;
        }

        public static Guid GetGuid(object value)
        {
            Guid guidValue = Guid.Empty;
            if (value != null && !Convert.IsDBNull(value))
            {
                Guid.TryParse(value.ToString(), out guidValue);
            }

            return guidValue;
        }
    }
}
