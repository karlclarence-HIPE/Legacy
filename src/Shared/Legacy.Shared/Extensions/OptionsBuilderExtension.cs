using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Legacy.Shared.Extensions;

public static class OptionsBuilderExtension
{
    public static OptionsBuilder<TOptions> ValidateFluentValidation<TOptions>(this OptionsBuilder<TOptions> builder) 
        where TOptions : class
    {
        return builder;
    }
}
