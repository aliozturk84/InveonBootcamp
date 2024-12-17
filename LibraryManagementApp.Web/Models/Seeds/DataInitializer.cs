using LibraryManagementApp.Web.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagementApp.Web.Models.Seeds
{
    public static class DataInitializer
    {
        public static async Task SeedRolesAndUsersAsync(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            // Rolleri Seed Et
            if (!roleManager.Roles.Any())
            {
                var roles = new List<string> { "Admin", "Ziyaretci" };

                foreach (var roleName in roles)
                {
                    var role = new AppRole { Name = roleName, NormalizedName = roleName.ToUpper() };
                    await roleManager.CreateAsync(role);
                }
            }

            // Kullanıcıları Seed Et
            if (!userManager.Users.Any())
            {
                // Admin Kullanıcısı
                var adminUser = new AppUser
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }

                // Normal Kullanıcı
                var normalUser = new AppUser
                {
                    UserName = "ziyaretci",
                    Email = "ziyaretci@hotmail.com",
                    EmailConfirmed = true
                };

                result = await userManager.CreateAsync(normalUser, "Ziyaretci123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(normalUser, "Ziyaretci");
                }
            }
        }
    }
}
