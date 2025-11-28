using Domain.Entities.User;
using System.IdentityModel.Tokens.Jwt;

namespace Domain.Abstracts
{
    public interface IAuthService
    {
        Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user);
    }
}
