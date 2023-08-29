using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MStore.Core.Entities.Identity;
using MStore.Core.IService;

namespace MStore.Service
{
    public class TokenService : ITokenService
    {

        public TokenService(IConfiguration configration)
        {
            Configration = configration;
        }

        public IConfiguration Configration { get; }

        public async Task<string> CreateToken(AppUser user, UserManager<AppUser> userManager)
        {
            var authCliams = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.DisplayName)

            };
            var roles = await userManager.GetRolesAsync(user);
            foreach(var role in roles)
                authCliams.Add(new Claim(ClaimTypes.Role, role));

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configration["JWT:Key"]));

            var token = new JwtSecurityToken(
                issuer: Configration["JWT:ValidIssuer"],
                audience: Configration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(double.Parse(Configration["JWT:ExpirationDays"])),
                claims: authCliams,
                signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
