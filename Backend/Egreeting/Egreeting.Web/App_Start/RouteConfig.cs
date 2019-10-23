using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Egreeting.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            string[] nameSpaceFrontend = { "EGreeting.Web.Controllers.Frontend" };
            string[] nameSpaceAdmin = { "EGreeting.Web.Controllers.Admin" };
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Admin",
                url: "admin/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                null,
                nameSpaceAdmin
            );
            routes.MapRoute(
                name: "Frontend",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                null,
                nameSpaceFrontend
            );


        }
    }
}
