using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Core.Constants;

namespace Northwind.Web.Configuration
{
    internal static class UserStoreConfiguration
    {
        // Just some data for testing purposes.
        private const string TestAdminUserEmail = "admin@admin.com";
        private const string TestAdminUserPassword = "Admin123";

        internal static async Task SeedDataAsync(IServiceProvider serviceProvider)
        {
            await CreateRole(serviceProvider, UserRoles.User);
            await CreateRole(serviceProvider, UserRoles.Admin);

            var adminId = await EnsureUser(serviceProvider);
            await EnsureRole(serviceProvider, adminId, UserRoles.Admin);
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(TestAdminUserEmail);
            if (user == null)
            {
                user = new IdentityUser
                {
                    Email = TestAdminUserEmail,
                    UserName = TestAdminUserEmail,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(user, TestAdminUserPassword);
            }

            return user.Id;
        }

        private static async Task CreateRole(IServiceProvider serviceProvider, string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        private static async Task EnsureRole(IServiceProvider serviceProvider, string userId, string role)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
            var user = await userManager.FindByIdAsync(userId);

            await userManager.AddToRoleAsync(user, role);
        }
    }
}
