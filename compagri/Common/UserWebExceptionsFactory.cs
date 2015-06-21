using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;

namespace CompAgri.Common
{
    public static class WebExceptionsFactory
    {
        public static HttpResponseException GetUserDuplicatedException()
        {
            var message = new HttpResponseMessage(HttpStatusCode.InternalServerError);

            message.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                Message = "There is an user with the same UserName or Email",
                UserDuplicated = true
            }));

            message.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return new HttpResponseException(message);
        }
    }
}
