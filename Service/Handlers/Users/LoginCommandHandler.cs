using Domain.Abstracts;
using Domain.Commands.User;
using Domain.DTOs.User;
using Domain.Entities.User;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace Service.Handlers.Users
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, SimpleResult<AuthResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthService _authService;

        public LoginCommandHandler(UserManager<ApplicationUser> userManager, IAuthService authService)
        {
            this._userManager = userManager;
            this._authService = authService;
        }
        public async Task<SimpleResult<AuthResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var authModel = new AuthResponse();

            var user = await _userManager.Users.Include(u => u.UserDetails).FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            if (user is null)
            {
                return SimpleResult<AuthResponse>.Failure(ErrorCode.InvalidCredentials, "Email or PassWord is Invalid");
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid)
            {
                return SimpleResult<AuthResponse>.Failure(ErrorCode.InvalidCredentials, "Email or PassWord is Invalid");
            }
            var jwtSecurityToken = await _authService.CreateJwtToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);

            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user?.Email;
            authModel.FirstName = user?.UserDetails?.FirstName;
            authModel.LastName = user?.UserDetails?.LastName;
            authModel.Id = user.Id;
            authModel.UserName = user.UserDetails?.UserName ?? string.Empty;
            authModel.IsSuccess = true;
            authModel.Message = "Login successful";
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.Roles = rolesList.ToList();

            return SimpleResult<AuthResponse>.Success(authModel, "Login successful");
        }
    }
}
