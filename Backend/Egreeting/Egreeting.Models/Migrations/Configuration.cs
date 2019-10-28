namespace Egreeting.Models.Migrations
{
    using Bogus;
    using Egreeting.Models.AppContext;
    using Egreeting.Models.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<Egreeting.Models.AppContext.EgreetingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Egreeting.Models.AppContext";
        }

        protected override void Seed(Egreeting.Models.AppContext.EgreetingContext context1)
        {
        }
    }
}
