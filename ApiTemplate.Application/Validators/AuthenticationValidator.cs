using ApiTemplate.Application.Contracts;
using FluentValidation;

namespace ApiTemplate.Application.Validators;

/// <summary>
/// Authentication request validator.
/// </summary>
public class AuthenticationValidator : AbstractValidator<AuthenticateRequestContract>
{
    /// <summary>
    /// Constructor
    /// </summary>
    public AuthenticationValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}
