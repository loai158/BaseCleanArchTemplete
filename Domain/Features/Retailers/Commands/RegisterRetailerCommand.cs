using Domain.DTOs.User;
using Domain.Exceptions;
using MediatR;

namespace Domain.Features.Retailers.Commands
{
    public class RegisterRetailerCommand : IRequest<SimpleResult<AuthResponse>>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string ShopName { get; set; } = null!;

        // Address
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Area { get; set; } = null!;
    }
}
