using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using MusicApp.Models;
using Owin;

[assembly: OwinStartupAttribute(typeof(MusicApp.Startup))]
namespace MusicApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            SeedRolesAndUsers();
        }
        private void SeedRolesAndUsers()
        {
            var context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            
            if (!roleManager.RoleExists("User"))
            {
                var newRole = new IdentityRole();
                newRole.Name = "User";
                roleManager.Create(newRole);
            }

            if (!roleManager.RoleExists("Admin"))
            {
                var newRole = new IdentityRole();
                newRole.Name = "Admin";
                roleManager.Create(newRole);

                var newUser = new ApplicationUser();
                newUser.UserName = "admin@gmail.com";
                newUser.Email = "admin@gmail.com";

                var result = userManager.Create(newUser, "Pa55word!");
                if (result.Succeeded)
                {
                    userManager.AddToRole(newUser.Id, "Admin");
                }


            }
        }
    }
}
