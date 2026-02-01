using FluentValidation;
using ProductivIO.Backend.DTOs.Auth;

namespace ProductivIO.Backend.Validations
{
    public class RegisterValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
        }
    }
}