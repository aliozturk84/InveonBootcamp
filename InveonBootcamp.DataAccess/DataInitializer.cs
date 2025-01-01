using InveonBootcamp.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.DataAccess
{
    public static class DataInitializer
    {
        public static async Task SeedRolesAndUsersAsync(
            RoleManager<Role> roleManager,
            UserManager<User> userManager)
        {
            // Rolleri Seed Et
            if (!roleManager.Roles.Any())
            {
                var roles = new List<string> { "Eğitmen" }; // Yeni roller eklenebilir
                foreach (var roleName in roles)
                {
                    var role = new Role { Name = roleName, NormalizedName = roleName.ToUpper() };
                    await roleManager.CreateAsync(role);
                }
            }

            // Kullanıcıları Seed Et
            if (!userManager.Users.Any())
            {
                // User1
                var user1 = new User
                {
                    UserName = "user1",
                    Email = "user1@gmail.com",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(user1, "User123!");
                if (result.Succeeded)
                {
                    // Rol atanabilir (opsiyonel)
                }

                // User2
                var user2 = new User
                {
                    UserName = "user2",
                    Email = "user2@gmail.com",
                    EmailConfirmed = true
                };
                result = await userManager.CreateAsync(user2, "User123!");
                if (result.Succeeded)
                {
                    // Rol atanabilir (opsiyonel)
                }

                // User3 (Eğitmen rolü atanıyor)
                var user3 = new User
                {
                    UserName = "user3",
                    Email = "user3@gmail.com",
                    EmailConfirmed = true
                };
                result = await userManager.CreateAsync(user3, "User123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user3, "Eğitmen");
                }
            }
        }
    }
}