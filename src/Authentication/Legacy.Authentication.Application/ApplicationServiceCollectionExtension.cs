using FluentValidation;
using Legacy.Authentication.Application.Database;
using Legacy.Authentication.Application.Services.Token;
using Legacy.Authentication.Application.Services.User;
using Legacy.Shared.Constants;
using Legacy.Shared.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Legacy.Authentication.Application;

public static class ApplicationServiceCollectionExtension
{
    public static IServiceCollection AddApplicationLayer<TOptions>(this IServiceCollection services) 
        where TOptions : class
    { 
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        var sectionName = typeof(TOptions).GetField(SharedConstant.SectionNamePlaceHolder)
            ?.GetRawConstantValue()
            ?.ToString();

        services.AddValidatorsFromAssemblyContaining<IApplicationMarker>(ServiceLifetime.Transient);

        services.AddOptions<TOptions>()
            .BindConfiguration(sectionName!)
            .ValidateFluentValidation()
            .ValidateOnStart();

        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
        return services;
    }
}
