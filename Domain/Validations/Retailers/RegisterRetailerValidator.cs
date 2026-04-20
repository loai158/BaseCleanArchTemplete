using Domain.Features.Retailers.Commands;
using FluentValidation;

namespace Domain.Validations.Retailers
{
    public class RegisterRetailerValidator : AbstractValidator<RegisterRetailerCommand>
    {
        public RegisterRetailerValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);

            RuleFor(x => x.Email)
                .NotEmpty().EmailAddress().WithMessage("Valid email is required");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8).WithMessage("Password must be at least 8 characters")
                .Matches("[A-Z]").WithMessage("Password must contain uppercase letter")
                .Matches("[0-9]").WithMessage("Password must contain a number");

            RuleFor(x => x.PhoneNumber).NotEmpty();
            RuleFor(x => x.ShopName).NotEmpty().MaximumLength(100);

            // Address
            RuleFor(x => x.Street).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.Area).NotEmpty();
        }
    }
}
