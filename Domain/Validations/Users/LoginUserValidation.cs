using Domain.Commands.User;
using FluentValidation;

namespace Domain.Validations.Users
{
    public class LoginUserValidation : AbstractValidator<LoginCommand>
    {
        public LoginUserValidation()
        {
            RuleFor(x => x.Email)
                  .NotEmpty().WithMessage("Email is required.")
                  .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}
