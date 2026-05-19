using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Legacy.Shared.Validator;

public class ValidateOptions<TOptions>(string? name, IServiceProvider provider) : IValidateOptions<TOptions> where TOptions : class
{
    public ValidateOptionsResult Validate(string? optionName, TOptions options)
    {
        if (name != optionName)
        {
            return ValidateOptionsResult.Skip;
        }

        ArgumentNullException.ThrowIfNull(options);

        using var scope = provider.CreateScope();
        var validator = scope.ServiceProvider.GetRequiredService<IValidator<TOptions>>();

        var result = validator.Validate(options);

        if (result.IsValid)
        {
            return ValidateOptionsResult.Success;
        }

        var type = options.GetType().Name;

        var errors = result.Errors.Select(error => $"Validation failed for {type}.{error.ErrorMessage}").ToArray();

        return ValidateOptionsResult.Fail(errors);
    }
}
