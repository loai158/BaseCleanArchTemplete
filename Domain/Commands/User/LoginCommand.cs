using Domain.DTOs.User;
using Domain.Exceptions;
using MediatR;

namespace Domain.Commands.User
{
    public class LoginCommand : IRequest<SimpleResult<AuthResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
