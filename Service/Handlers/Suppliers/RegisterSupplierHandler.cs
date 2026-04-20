using Domain.Abstracts;
using Domain.DTOs.User;
using Domain.Exceptions;
using Domain.Features.Suppliers.Commands;
using MediatR;

namespace Service.Handlers.Suppliers
{
    public class RegisterSupplierHandler : IRequestHandler<RegisterSupplierCommand, SimpleResult<AuthResponse>>
    {
        private readonly IAuthService _authService;

        public RegisterSupplierHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<SimpleResult<AuthResponse>> Handle(RegisterSupplierCommand request, CancellationToken cancellationToken)
        {
            return await _authService.RegisterSupplierAsync(request);
        }
    }
}
