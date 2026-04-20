using Domain.Features.Suppliers.Commands;
using FluentValidation;

namespace Domain.Validations.Suppliers
{
    public class RegisterSupplierValidator : AbstractValidator<RegisterSupplierCommand>
    {
        public RegisterSupplierValidator()
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
            RuleFor(x => x.StoreName).NotEmpty().MaximumLength(100);

            // Address
            RuleFor(x => x.Street).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.Area).NotEmpty();
        }
    }
}
