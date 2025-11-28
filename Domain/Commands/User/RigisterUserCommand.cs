using Domain.DTOs.User;
using Domain.Exceptions;
using MediatR;

namespace Domain.Commands.User
{
    public class RigisterUserCommand : IRequest<SimpleResult<AuthResponse>>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
