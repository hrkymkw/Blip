using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Text;

namespace Blip.Web.Filters
{
    public class LogFilterAttribute : ActionFilterAttribute
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);        
        private const string StopwatchKey = "LogStopWatch";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (log.IsDebugEnabled)
            {
                var logWatch = Stopwatch.StartNew();
                filterContext.HttpContext.Items.Add(StopwatchKey, logWatch);

                var message = new StringBuilder();
                message.Append(string.Format("Executing {0}/{1}",
                    filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                    filterContext.ActionDescriptor.ActionName));

                log.Debug(message);
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (log.IsDebugEnabled)
            {
                if (filterContext.HttpContext.Items[StopwatchKey] != null)
                {
                    var logWatch = (Stopwatch)filterContext.HttpContext.Items[StopwatchKey];
                    logWatch.Stop();

                    long timeLapse = logWatch.ElapsedMilliseconds;

                    var message = new StringBuilder();
                        message.Append(string.Format("Finished executing {0}/{1} - time spent {2}",
                            filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                            filterContext.ActionDescriptor.ActionName,
                            timeLapse));

                    log.Debug(message);
                    filterContext.HttpContext.Items.Remove(StopwatchKey);
                }
            }
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }
    }
}