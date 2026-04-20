using Domain.Abstracts;
using Domain.Commands.User;
using Domain.DTOs.User;
using Domain.Exceptions;
using MediatR;

namespace Service.Handlers.Users
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, SimpleResult<AuthResponse>>
    {
        private readonly IAuthService _authService;

        public LoginCommandHandler(IAuthService authService)
        {
            this._authService = authService;
        }
        public async Task<SimpleResult<AuthResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return await _authService.LoginAsync(request);
        }
    }
}
