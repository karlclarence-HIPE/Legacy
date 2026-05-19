using System.Data;
using FluentValidation;

namespace Legacy.Authentication.Application.Configuration.Validators;

public class JwtOptionsValidator : AbstractValidator<JwtOptions>
{
    public JwtOptionsValidator()
    {
        RuleFor(j => j.Issuer)
            .NotEmpty();

        RuleFor(j => j.SigningKey)
            .NotEmpty();

        RuleFor(j => j.ExpirationTime)
            .NotEmpty();

        RuleFor(j => j.RefreshDuration)
            .NotEmpty();
    }
}
