using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SelectAndRepair.Common;
using SelectAndRepair.Data.Models;
using System;
using System.Threading.Tasks;

namespace SelectAndRepair.Data.Seeding
{
    internal class AdminSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var email = "angel.aleksandrov06@gmail.com";

            var user = await userManager.FindByEmailAsync(email);

            if (user != null)
            {
                return;
            }

            var admin = new ApplicationUser()
            {
                Email = email,
                UserName = email
            };

            await userManager.CreateAsync(admin, "Sb123456");
            await userManager.AddToRoleAsync(admin, GlobalConstants.AdministratorRoleName);
        }
    }
}
