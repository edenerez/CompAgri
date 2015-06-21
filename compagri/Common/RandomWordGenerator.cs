using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace CompAgri.Common
{
    public class RandomWordGenerator
    {
        private static string DEFAULT_SALT_POSIBLE_CHARS = "ABCDEFGHIJKLMNÑOQRSTUWXYZabcefghijklmnñopqrstuwxyz1234567890,.-_:;";

        public static string GenerateRandomString(int lenght)
        {
            var builder = new StringBuilder(lenght);
            var randomGenerator = new RNGCryptoServiceProvider();

            var indexes = new byte[lenght];
            randomGenerator.GetBytes(indexes);
            foreach (byte index in indexes)
            {
                builder.Append(DEFAULT_SALT_POSIBLE_CHARS[index % DEFAULT_SALT_POSIBLE_CHARS.Length]);
            }
            return builder.ToString();
        }
    }
}