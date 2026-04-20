using Domain.Commands.User;
using Domain.DTOs.User;
using Domain.Entities.User;
using Domain.Exceptions;
using Domain.Features.Retailers.Commands;
using Domain.Features.Suppliers.Commands;
using System.IdentityModel.Tokens.Jwt;

namespace Domain.Abstracts
{
    public interface IAuthService
    {
        Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user);
        Task<SimpleResult<AuthResponse>> RegisterRetailerAsync(RegisterRetailerCommand request);
        Task<SimpleResult<AuthResponse>> RegisterSupplierAsync(RegisterSupplierCommand request);
        Task<SimpleResult<AuthResponse>> LoginAsync(LoginCommand request);
    }
}
