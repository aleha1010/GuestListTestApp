using System;
using System.Data.Entity.Migrations;
using System.Linq;
using GuestListTestApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GuestListTestApp.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (!context.Roles.Any())
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);

                var role = new IdentityRole {Name = Roles.Admin};
                manager.Create(role);

                role = new IdentityRole {Name = Roles.Guest };
                manager.Create(role);
            }

            if (!context.Users.Any())
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new Admin {UserName = "admin", Email = "admin@email.com"};

                manager.Create(user, "adminadmin");
                manager.AddToRole(user.Id, Roles.Admin);
            }
        }
    }
}