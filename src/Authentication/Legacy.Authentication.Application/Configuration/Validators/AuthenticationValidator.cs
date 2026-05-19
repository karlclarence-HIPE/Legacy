using FluentValidation;
using Legacy.Authentication.Application.Database;
namespace Legacy.Authentication.Application.Configuration.Validators;

public class AuthenticationValidator : AbstractValidator<AuthenticationModuleConfiguration>
{
    public AuthenticationValidator()
    {
        RuleFor(a => a.ModuleCode)
            .NotEmpty(); 
        
        RuleFor(a => a.ConnectionString)
            .NotEmpty();

        RuleFor(a => a.JwtOptions)
            .SetValidator(new JwtOptionsValidator());

        RuleFor(a => a.AllowedOriginis)
            .NotEmpty();
    }
}
