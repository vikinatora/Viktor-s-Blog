using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MyBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Utilities
{
    public static class ApplicationBuilderExtensions
    {
        private const string DefaultAdminPassword = "123123";
        private const string DefaultModeratorPassword = "123123";


        private static IdentityRole[] roles =
        {
            new IdentityRole("Administrator"),
            new IdentityRole("Moderator")
        };

        public static async void SeedDatabase(this IApplicationBuilder app)
        {
            var serviceFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            
            var scope = serviceFactory.CreateScope();

            using (scope)
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role.Name))
                    {
                        await roleManager.CreateAsync(role);
                    }
                }
                var user = await userManager.FindByNameAsync("Admin");
                if (user == null)
                {
                    user = new User()
                    {
                        UserName = "admin",
                        Email = "admin@example.com",
                        LockoutEnabled = false

                    };

                    await userManager.CreateAsync(user, DefaultAdminPassword);

                    await userManager.AddToRoleAsync(user, roles[0].Name);
                }

                var moderator = await userManager.FindByNameAsync("Moderator");
                if (moderator == null)
                {
                    moderator = new User()
                    {
                        UserName = "moderator",
                        Email = "moderator@mod.com"
                    };

                    await userManager.CreateAsync(moderator, DefaultModeratorPassword);
                    await userManager.AddToRoleAsync(moderator, roles[1].Name);
                }


            }


        }
    }
}
