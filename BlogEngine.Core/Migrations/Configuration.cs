using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using WebMatrix.WebData;
using System.Web.Security;


namespace BlogEngine.Core.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<BlogEngine.Core.Contexts.BlogContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BlogEngine.Core.Contexts.BlogContext context)
        {
            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "Users", "UserId", "UserName", autoCreateTables: true);

            if (!Roles.RoleExists("Administrator"))
                Roles.CreateRole("Administrator");

            if (!Roles.RoleExists("User"))
                Roles.CreateRole("User");

            if (!WebSecurity.UserExists("Admin"))
            {
                WebSecurity.CreateUserAndAccount("Admin", "password",
                    new
                    {
                        FirstName = "Jamie",
                        LastName = "van Walsum",
                        Email = "jvw@westnet.com.au",
                        CreatedDate = DateTime.Now
                    });
            }

            if (!Roles.GetRolesForUser("Admin").Contains("Administrator"))
            {
                Roles.AddUsersToRoles(new[] { "Admin" }, new[] { "Administrator", "User" });
            }

        }
    }
}
