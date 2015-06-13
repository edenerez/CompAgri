using System.Linq;
using System.Web.Http.Controllers;

namespace CompAgri.Common
{
    public sealed class WebApiAuthorizeFilter : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var someCode = (from h in actionContext.Request.Headers where h.Key == "Authorization" select h.Value.First()).FirstOrDefault();
            var token = System.Configuration.ConfigurationManager.AppSettings["apiToken"];
            return someCode == token;
        }
    }
}