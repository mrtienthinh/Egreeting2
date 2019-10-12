using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;

namespace Egreeting.Web.App_Start
{
    public class LogActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.RequestContext.RouteData.Values["Controller"];
            var action = filterContext.RequestContext.RouteData.Values["Action"];

            var loger = LogManager.GetLogger("EGreetingLog");
            var message = String.Format("Controller:{0} action:{1}", controller.ToString(), action.ToString());
            loger.Info(message);

            base.OnActionExecuting(filterContext);
        }
    }
}