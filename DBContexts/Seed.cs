using AtlantisPortals.API.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtlantisPortals.API.DBContexts
{
    public class Seed
    {

        public static void SeedUsers(UserManager<User> _userManager, RoleManager<Role> _roleManager)
        {

            if (!_userManager.Users.Any())
            {

                var roles = new List<Role>
                {
                    new Role{Name = "SuperAdmin"},
                    new Role{Name = "Admin"},
                    new Role{Name = "Manager"},
                    new Role{Name = "TeamLeader"},
                    new Role{Name = "Agent"}
                };

                foreach (var role in roles)
                {
                    _roleManager.CreateAsync(role).Wait();
                }

                var adminUser = new User
                {
                    UserName = "superadmin@pdq.com",
                    Email = "superadmin@pdq.com",
                    FullName = "Kenneth Holland",
                    PhoneNumber = "07957654152",
                    Created = DateTime.UtcNow,
                    LastActive = DateTime.UtcNow,
                };

                IdentityResult result = _userManager.CreateAsync(adminUser, "password").Result;

                if (result.Succeeded)
                {
                    var admin = _userManager.FindByEmailAsync("superadmin@pdq.com").Result;
                    _userManager.AddToRolesAsync(admin, new[] { "SuperAdmin", "Manager", "Agent" }).Wait();
                }
            }
        }
    }
}
