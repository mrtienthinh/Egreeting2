using Egreeting.Models.Models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Egreeting.Web.App_Start
{
    public static class ODataConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // New code:
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Category>("Categories");
            builder.EntitySet<Ecard>("Ecards");
            builder.EntitySet<EgreetingUser>("EgreetingUsers");
            builder.EntitySet<EgreetingRole>("EgreetingRoles");
            builder.EntitySet<Feedback>("Feedbacks");
            builder.EntitySet<Order>("Orders");
            builder.EntitySet<OrderDetail>("OrderDetails");
            builder.EntitySet<Payment>("Payments");
            builder.EntitySet<ScheduleSender>("ScheduleSenders");
            builder.EntitySet<Subcriber>("Subcribers");
            //Moar!

            //config.MapHttpAttributeRoutes();
            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: "odata",
                model: builder.GetEdmModel());
        }
    }
}