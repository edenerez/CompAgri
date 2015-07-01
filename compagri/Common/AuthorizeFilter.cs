using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace CompAgri.Common
{
    public sealed class AuthorizeFilter : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var skipAuthorization = actionContext.ActionDescriptor.GetFilters().OfType<AllowAnonymousAttribute>().Count() != 0;
            skipAuthorization = skipAuthorization || actionContext.ActionDescriptor.ControllerDescriptor.GetFilters().OfType<AllowAnonymousAttribute>().Count() != 0;

            if (skipAuthorization)
            {
                return true;
            }

            var token = UserUtils.GetUserToken(actionContext.Request);

            if (token == null)
            {
                return false;
            }

            using (var db = new CompAgriConnection())
            {
                var user = db.User.FirstOrDefault(u => u.Token == token);
                if (user == null)
                {
                    return false;
                }

                return UserHasPermissionToAction(user, actionContext);
            }
        }        

        private bool UserHasPermissionToAction(User user, HttpActionContext actionContext)
        {
            var controllerName = actionContext.ControllerContext.ControllerDescriptor.ControllerName;
            var actionName = actionContext.ActionDescriptor.ActionName;

            using (var db = new CompAgriConnection())
            {
                var permissions = db.UserProfilePermission.Where(up => up.UserProfile_Id == user.UserProfile_Id && up.ControllerName == controllerName && up.ActionName == actionName);

                if (permissions.Count() == 0)
                {
                    return true;
                }
                else if (permissions.FirstOrDefault(up => up.Deny == true) != null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}