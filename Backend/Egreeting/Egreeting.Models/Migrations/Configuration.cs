namespace Egreeting.Models.Migrations
{
    using Egreeting.Models.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
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

            //context.EgreetingRoles.AddOrUpdate(
            //    new Models.EgreetingRole { EgreetingRoleName = "Admin" },
            //    new Models.EgreetingRole { EgreetingRoleName = "User" },
            //    new Models.EgreetingRole { EgreetingRoleName = "Subcriber" }
            //);

            context.Categories.AddOrUpdate(
                new Models.Category { CategoryName = "Birthday", CategorySlug = "birthday" },
                new Models.Category { CategoryName = "Wedding", CategorySlug = "wedding" },
                new Models.Category { CategoryName = "New year", CategorySlug = "new-year" },
                new Models.Category { CategoryName = "Festivals", CategorySlug = "festivals" }
            );

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole { Name = "Admin", EgreetingRole = new Models.EgreetingRole { EgreetingRoleName = "Admin" } };
                manager.Create(role);
            }
            if (!context.Roles.Any(r => r.Name == "User"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole { Name = "User", EgreetingRole = new Models.EgreetingRole { EgreetingRoleName = "User" } };
                manager.Create(role);
            }
            if (!context.Roles.Any(r => r.Name == "Subcriber"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole { Name = "Subcriber", EgreetingRole = new Models.EgreetingRole { EgreetingRoleName = "Subcriber" } };
                manager.Create(role);
            }
            if (!context.Users.Any(u => u.UserName == "mrtienthinh@gmail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "mrtienthinh@gmail.com", Email = "mrtienthinh@gmail.com", EgreetingUser = new EgreetingUser { Email = "mrtienthinh@gmail.com", FirstName = "Tien Thinh", LastName = "Nguyen" } };

                manager.Create(user, "123456aA@");
                manager.AddToRole(user.Id, "Admin");
            }
            if (!context.Users.Any(u => u.UserName == "admin@gmail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "admin@gmail.com", Email = "admin@gmail.com", EgreetingUser = new EgreetingUser { Email = "admin@gmail.com", FirstName = "Tien Thinh", LastName = "Nguyen" } };

                manager.Create(user, "123456aA@");
                manager.AddToRole(user.Id, "Admin");
            }
            if (!context.Users.Any(u => u.UserName == "user@gmail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "user@gmail.com", Email = "user@gmail.com", EgreetingUser = new EgreetingUser { Email = "user@gmail.com", FirstName = "Tien Thinh", LastName = "Nguyen" } };

                manager.Create(user, "123456aA@");
                manager.AddToRole(user.Id, "User");
            }
            if (!context.Users.Any(u => u.UserName == "subcriber@gmail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "subcriber@gmail.com", Email = "subcriber@gmail.com", EgreetingUser = new EgreetingUser { Email = "subcriber@gmail.com", FirstName = "Tien Thinh", LastName = "Nguyen" } };

                manager.Create(user, "123456aA@");
                manager.AddToRole(user.Id, "Subcriber");
            }
        }
    }
}
