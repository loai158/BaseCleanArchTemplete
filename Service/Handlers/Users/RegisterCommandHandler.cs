using Domain.Abstracts;
using Domain.Commands.User;
using Domain.DTOs.User;
using Domain.Entities.User;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace Service.Handlers.Users
{
    public class RegisterCommandHandler : IRequestHandler<RigisterUserCommand, SimpleResult<AuthResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthService _authService;
        private readonly IUnitOfWork _unitOfWork;
        public RegisterCommandHandler(UserManager<ApplicationUser> userManager, IAuthService authService, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            this._authService = authService;
            _unitOfWork = unitOfWork;
        }
        public async Task<SimpleResult<AuthResponse>> Handle(RigisterUserCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var existingUser = await _userManager.FindByEmailAsync(request.Email);
                if (existingUser != null)
                    return SimpleResult<AuthResponse>.Failure(ErrorCode.EmailAlreadyExists);

                var user = new ApplicationUser
                {
                    UserName = request.Email,
                    Email = request.Email,
                };

                var result = await _userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return SimpleResult<AuthResponse>.Failure(ErrorCode.InvalidPassword,
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                }

                // Create UserDetails
                var userDetails = new UserDetails();
                userDetails.CreateUserDetails(
                    firstName: request.FirstName,
                    lastName: request.LastName,
                    userName: request.Username,
                    userId: user.Id
                );

                await _unitOfWork.Repository<UserDetails>().AddAsync(userDetails);

                // Save everything inside transaction
                await _unitOfWork.SaveChangesAsync();

                // Create token
                var jwtSecurityToken = await _authService.CreateJwtToken(user);

                await _unitOfWork.CommitTransactionAsync();

                var response = new AuthResponse
                {
                    Email = user.Email,
                    IsAuthenticated = true,
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    FirstName = userDetails.FirstName,
                    LastName = userDetails.LastName,
                    Id = user.Id,
                    Roles = new List<string> { "User" },
                    IsSuccess = true,
                    ExpiresOn = jwtSecurityToken.ValidTo,
                    Message = "Registration successful",
                    UserName = userDetails.UserName,
                };

                return SimpleResult<AuthResponse>.Success(response);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return SimpleResult<AuthResponse>.Failure(
                    ErrorCode.InternalServerError,
                    $"Registration failed: {ex.Message}"
                );
            }
        }
    }
}
