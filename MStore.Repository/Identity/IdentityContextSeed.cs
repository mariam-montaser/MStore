using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MStore.Core.Entities.Identity;

namespace MStore.Repository.Identity
{
    public class IdentityContextSeed
    {
        public async static Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Mariam Montaser",
                    Email = "mariammontaser@gmail.com",
                    UserName = "mariammontaser",
                    PhoneNumber = "01234567890"
                };
                await userManager.CreateAsync(user, "P@$$w0rd");
            }
        }
    }
}
