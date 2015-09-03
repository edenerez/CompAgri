using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace CompAgri.Controllers
{
    [Common.AuthorizeFilter]
    public class LogController : ApiController
    {
        public static Dictionary<string, bool> downloadLinks = new Dictionary<string, bool>();
        public const int DOWNLOAD_LINK_TOKEN_SIZE = 40;

        public string Get()
        {
            if (!Common.UserUtils.GetUser(Request).UserProfile.CanDownloadFullLog)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            var downloadLink = Common.RandomWordGenerator.GenerateRandomString(DOWNLOAD_LINK_TOKEN_SIZE);

            downloadLinks.Add(downloadLink, true);
            return downloadLink;
        }

        [AllowAnonymous]
        public HttpResponseMessage Get(string id)
        {
            if (downloadLinks.ContainsKey(id) && downloadLinks[id])
            {
                downloadLinks.Remove(id);
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                var stream = new FileStream(Common.CSVLogger.CSVLogger.Instance.path, FileMode.Open);
                result.Content = new StreamContent(stream);
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = "log.csv";

                return result;
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
        }
    }
}
