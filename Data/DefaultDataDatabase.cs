using Crowfounding.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crowfounding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Crowfounding.Data
{
    public class DefaultDataDatabase
    {
        public static async Task Initialize(ApplicationDbContext context,
                                            UserManager<User> userManager,
                                           RoleManager<IdentityRole> roleManager,
                                           IOptions<EmailSettings> emailSettings)
        {
            EmailSettings settings = emailSettings.Value;

            context.Database.EnsureCreated();
            string[] roles = { "Admin", "Member", "SuperAdmin" };
            foreach(var role in roles)
            {
                if(await roleManager.FindByNameAsync(role) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            if(await userManager.FindByEmailAsync(settings.SenderEmail) == null)
            {
                var user = new User
                {
                    UserName = settings.SenderEmail,
                    Email = settings.SenderEmail,
                    EmailConfirmed = true,
                    Name = "SuperAdmin",
                    DateRegister = DateTime.Now,
                    DataLogin = DateTime.Now
                };
                var result = await userManager.CreateAsync(user);
                if(result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, settings.Password);
                    await userManager.AddToRolesAsync(user, roles);
                }
            }

           

        }

    }
}
