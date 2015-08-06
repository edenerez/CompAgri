using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace CompAgri.Common.CSVLogger
{
    public class CSVLogger
    {
        public const string SEPARATOR = ";";
        private static CSVLogger instance;
        private string path;
        private CSVLogger()
            : this(ConfigurationManager.AppSettings["log-location"])
        {
        }

        private CSVLogger(string path)
        {
            this.path = HttpContext.Current.Server.MapPath(path);
            if (!File.Exists(path))
            {
                AddHeaderToLog();
            }
        }

        public static CSVLogger Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CSVLogger();
                }

                return instance;
            }
        }

        public void LogAction(System.Web.Http.Filters.HttpActionExecutedContext actionExecutedContext, ActionLogger actionLogger)
        {
            var log = new CSVLog
            {
                Action = actionExecutedContext.ActionContext.ActionDescriptor.ActionName,
                Controller = actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName,
                Date = DateTime.UtcNow,
                Description = actionLogger.getDescription(actionExecutedContext),
                IP = IPUtils.GetClientIp(actionExecutedContext.Request)
            };

            var user = Common.UserUtils.GetUser(actionExecutedContext.Request);
            if (user != null)
            {
                log.UserEmail = user.Email;
                log.Username = user.UserName;
            }

            Log(log);
        }

        public void Log(CSVLog log)
        {
            lock (this)
            {
                var line = String.Join(SEPARATOR, log.GetType().GetProperties().Select(t => t.GetValue(log)));
                WriteLine(line);
            }
        }

        private void AddHeaderToLog()
        {

            lock (this)
            {
                var header = String.Join(SEPARATOR, typeof(CSVLog).GetProperties().Select(t => t.Name));
                WriteLine(header);
            }
        }

        private void WriteLine(string line)
        {
            File.AppendAllText(path, line);
            File.AppendAllText(path, "\n");
        }
    }
}