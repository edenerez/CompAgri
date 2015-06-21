using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace CompAgri.Common
{
    public class UserWrongPasswordException : HttpResponseException
    {
        public UserWrongPasswordException()
            : base(HttpStatusCode.Forbidden)
        {
        }
    }
}