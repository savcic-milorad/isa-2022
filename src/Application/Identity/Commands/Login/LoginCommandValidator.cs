using FluentValidation;

namespace TransfusionAPI.Application.Identity.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(v => v.UserName)
            .EmailAddress()
            .NotEmpty();
        RuleFor(v => v.Password)
            .NotEmpty();
    }
}
