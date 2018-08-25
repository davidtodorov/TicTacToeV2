using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.DependencyInjection;
using TicTacToe.Common.Constants;
using TicTacToe.Models;

namespace TicTacToeWeb.Extensions
{
    public static class AppBuilderSeedExtension
    {
        private const string DefaultAdminPassword = "123";

        private static readonly IdentityRole[] roles =
        {
            new IdentityRole(RoleConstants.ADMIN_ROLE),
            new IdentityRole(RoleConstants.PLAYER_ROLE)
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
                
                //todo: 
                var admin = await userManager.FindByNameAsync("Admin");
                var users = userManager.Users;
                if (admin == null)
                {
                    admin = new User()
                    {
                        UserName = "admin@abv.bg",
                        Email = "admin@abv.bg",
                        FirstName = "Admin",
                        LastName = "Admin",
                    };

                    var adminCreated = await userManager.CreateAsync(admin, DefaultAdminPassword);
                    var roleResult = await userManager.AddToRoleAsync(admin, roles[0].Name);
                }
            }
        }
    }
}
