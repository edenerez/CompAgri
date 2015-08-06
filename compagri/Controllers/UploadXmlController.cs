using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace CompAgri.Controllers
{
    [Common.AuthorizeFilter]
    public class UploadXmlController : ApiController
    {
        [Common.CSVLogger.ActionLogger(description: "Uploaded a Tree")]
        public void Post()
        {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/XmlFiles"), postedFile.FileName);
                    postedFile.SaveAs(filePath);

                    Bll.CompAgriBll.UploadFile(filePath);
                }

            }
        }

    }
}

