using ColorexApp.Application.Features.Auth.Commands;
using FluentValidation;

namespace ColorexApp.Application.Validators.Auth;

public sealed class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}
