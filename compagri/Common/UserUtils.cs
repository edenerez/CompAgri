using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace CompAgri.Common
{
    public static class UserUtils
    {
        public static User GetUser(this HttpRequestMessage request)
        {
            var token = GetUserToken(request);

            if (token == null)
            {
                return null;
            }

            using (var db = new CompAgriConnection())
            {
                return db.User.FirstOrDefault(u => u.Token == token);
            }
        }

        public static string GetUserToken(HttpRequestMessage request)
        {
            if (!request.Headers.Contains("CompagriUserToken"))
            {
                return null;
            }

            return request.Headers.GetValues("CompagriUserToken").FirstOrDefault();
        }

    }
}