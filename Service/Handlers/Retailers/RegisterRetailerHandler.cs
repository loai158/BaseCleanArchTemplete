using Domain.Abstracts;
using Domain.DTOs.User;
using Domain.Exceptions;
using Domain.Features.Retailers.Commands;
using MediatR;

namespace Service.Handlers.Retailers
{
    public class RegisterRetailerHandler : IRequestHandler<RegisterRetailerCommand, SimpleResult<AuthResponse>>
    {
        private readonly IAuthService _authService;

        public RegisterRetailerHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<SimpleResult<AuthResponse>> Handle(RegisterRetailerCommand request, CancellationToken cancellationToken)
        {
            return await _authService.RegisterRetailerAsync(request);
        }
    }
}
