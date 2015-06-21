using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompAgri.Common
{
    public class TokenUtils
    {
        private const int DEFAULT_TOKEN_LENGTH = 20;
        public static string GenerateToken()
        {
            return RandomWordGenerator.GenerateRandomString(DEFAULT_TOKEN_LENGTH);
        }
    }
}