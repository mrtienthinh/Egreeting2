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
            var faker = new Faker("en");
            using (var context = new EgreetingContext())
            {
                //20 category;
                var categories = new List<Category>();
                categories.Add(new Category { CategoryID = 1, CategoryName = "Birthday", CategorySlug = "birthday" });
                categories.Add(new Category { CategoryID = 2, CategoryName = "Wedding", CategorySlug = "wedding" });
                categories.Add(new Category { CategoryID = 3, CategoryName = "New year", CategorySlug = "new-year" });
                categories.Add(new Category { CategoryID = 4, CategoryName = "Festivals", CategorySlug = "festivals" });

                for (int i = 5; i < 20; i++)
                {
                    categories.Add(new Category
                    {
                        CategoryID = i,
                        CategoryName = "Category " + i,
                        CategorySlug = "Category-" + i,
                        CreatedDate = faker.Date.Past(),
                    });
                }
                context.Set<Category>().AddRange(categories);
                context.SaveChanges();
            }

            //3 role
            using (var context = new EgreetingContext())
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole
                {
                    Name = "Admin",
                    EgreetingRole = new EgreetingRole
                    {
                        EgreetingRoleID = 1,
                        EgreetingRoleName = "Admin",
                        CreatedDate = faker.Date.Past(),
                    }
                };
                manager.Create(role);
                role = new ApplicationRole
                {
                    Name = "User",
                    EgreetingRole = new EgreetingRole
                    {
                        EgreetingRoleID = 2,
                        EgreetingRoleName = "User",
                        CreatedDate = faker.Date.Past(),
                    }
                };
                manager.Create(role);
                role = new ApplicationRole
                {
                    Name = "Subcriber",
                    EgreetingRole = new EgreetingRole
                    {
                        EgreetingRoleID = 3,
                        EgreetingRoleName = "Subcriber",
                        CreatedDate = faker.Date.Past(),
                    }
                };
                manager.Create(role);
            }
        }
    }
}
