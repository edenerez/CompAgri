using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace CompAgri.Common.CSVLogger
{
    public class ActionLogger : ActionFilterAttribute, IActionFilter
    {
        private string description;

        public ActionLogger()
            : this("Access to {0} action in {1}")
        {

        }

        public ActionLogger(string description)
        {
            this.description = description;
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
            this.LogAction(actionExecutedContext);
        }

        public override System.Threading.Tasks.Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, System.Threading.CancellationToken cancellationToken)
        {
            this.LogAction(actionExecutedContext);
            return base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
        }

        public string getDescription(HttpActionExecutedContext actionExecutedContext)
        {
            return String.Format(description, actionExecutedContext.ActionContext.ActionDescriptor
                .ActionName, actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName);
        }

        protected void LogAction(HttpActionExecutedContext actionExecutedContext)
        {
            CSVLogger.Instance.LogAction(actionExecutedContext, this);
        }
    }
}