using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompAgri.Common
{
    public class IPUtils
    {
        public static string GetClientIp(System.Net.Http.HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }

            return null;
        }
    }
}