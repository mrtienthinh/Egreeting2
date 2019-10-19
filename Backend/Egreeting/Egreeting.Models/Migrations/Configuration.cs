namespace Egreeting.Models.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Egreeting.Models.AppContext.EgreetingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Egreeting.Models.AppContext.EgreetingContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Categories.AddOrUpdate(
                new Models.Category { CategoryName = "Birthday", CategorySlug = "birthday"},
                new Models.Category { CategoryName = "Wedding", CategorySlug = "wedding" },
                new Models.Category { CategoryName = "New year", CategorySlug = "new-year" },
                new Models.Category { CategoryName = "Festivals", CategorySlug = "festivals" }
            );
            context.EgreetingRoles.AddOrUpdate(
                new Models.EgreetingRole { EgreetingRoleName = "Admin"},
                new Models.EgreetingRole { EgreetingRoleName = "User"},
                new Models.EgreetingRole { EgreetingRoleName = "Subcriber"}
            );
        }
    }
}
