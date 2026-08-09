using FluentValidation;
using TaskPilot.Api.DTOs.Auth;

namespace TaskPilot.Api.Validators.Auth;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.UsernameOrEmail)
            .NotEmpty();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}