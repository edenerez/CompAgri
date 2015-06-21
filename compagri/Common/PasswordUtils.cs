using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace CompAgri.Common
{
    public class PasswordUtils
    {
        private const int DEFAULT_SALT_SIZE = 512;
        public static string HashPassword(string password, string salt)
        {
            var hasher = SHA512.Create();
            var bytes = Encoding.Unicode.GetBytes(password + salt);
            var hashedData = hasher.ComputeHash(bytes);
            return Convert.ToBase64String(hashedData);
        }

        public static string GenerateSalt()
        {
            return RandomWordGenerator.GenerateRandomString(DEFAULT_SALT_SIZE);
        }
    }
}