using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MStore.Core.Entities.Identity;

namespace MStore.API.Extensions
{
    public static class UserManagerExtension
    {
        public async static Task<AppUser> FindByEmailWithAddressAsync(this UserManager<AppUser> userManager, ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email);
            var userWithAddress = userManager.Users.Include(u => u.Address).SingleOrDefaultAsync(u => u.Email == email);
            return await userWithAddress;
        }
    }
}
