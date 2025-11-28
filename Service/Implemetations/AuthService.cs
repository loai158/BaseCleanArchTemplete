using Domain.Abstracts;
using Domain.DTOs.User;
using Domain.Entities.Roles;
using Domain.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service.Implemetations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOptions<JWT> _jwt;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public AuthService(IOptions<JWT> jwt, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
            this._jwt = jwt;
            this._roleManager = roleManager;
        }
        public async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));

                var identityRole = await _roleManager.FindByNameAsync(role);
                if (identityRole != null)
                {
                    var permissions = await _roleManager.GetClaimsAsync(identityRole);
                    roleClaims.AddRange(permissions);
                }
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim("uid", user.Id),
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Value.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Value.Issuer,
                audience: _jwt.Value.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwt.Value.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
