using System;
using System.Security.Cryptography;
using System.Text;

namespace DXP.SmartConnectPickup.Common.Utils
{
    public static class FirstDataUCommHelpers
    {
        /// <summary>
        /// Gets unix timestamp.
        /// Guideline: https://www.tutorialspoint.com/how-to-get-the-unix-timestamp-in-chash
        /// </summary>
        /// <returns>System.Int64.</returns>
        public static long GetTimestamp()
        {
            return (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
        }

        /// <summary>
        /// Gets authorization.
        /// Guideline: https://firstdatanp-ucomgateway.apigee.io/get-started/api-security
        /// </summary>
        /// <param name="apiKey">The apiKey.</param>
        /// <param name="apiSecret">The apiSecret.</param>
        /// <param name="epochTimestamp">The epochTimestamp.</param>
        /// <param name="payload">The payload.</param>
        /// <returns>System.String.</returns>
        public static string GetAuthorization(string apiKey, string apiSecret, long epochTimestamp, string payload)
        {
            var separatedBy = ":";
            var stringBuilder = new StringBuilder();

            // Concatenate the API Key, Timestamp and Hashed payload (only if applicable) separated by colon ( : ) character
            stringBuilder.Append(apiKey).Append(separatedBy).Append(epochTimestamp);

            // If payload is not empty, hash the payload using SHA-256 and encode using Base64
            if (payload != null && payload.Trim() != "")
            {
                byte[] hashPayload = ComputeSha256Hash(payload);
                string base64Payload = Convert.ToBase64String(hashPayload);
                stringBuilder.Append(separatedBy).Append(base64Payload);
            }

            // Generate a HMAC for the concatenated message with API Secret using SHA-256 algorithm and encode using Base64
            string messageToSign = stringBuilder.ToString();
            byte[] hashHMAC = GetHashHMAC(Encoding.UTF8.GetBytes(apiSecret), Encoding.UTF8.GetBytes(messageToSign));
            return Convert.ToBase64String(hashHMAC);
        }

        private static byte[] ComputeSha256Hash(string rawData)
        {
            using SHA256 sha256Hash = SHA256.Create();
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            return bytes;
        }

        private static byte[] GetHashHMAC(byte[] key, byte[] message)
        {
            var hash = new HMACSHA256(key);
            return hash.ComputeHash(message);
        }
    }
}
