using FluentValidation;
using ProductivIO.Contracts.Requests.Auth;

namespace ProductivIO.Application.Validations;

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}
