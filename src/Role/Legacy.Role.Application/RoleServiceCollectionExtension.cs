using FluentValidation;
using Legacy.Shared.Constants;
using Microsoft.Extensions.DependencyInjection;

namespace Legacy.Role.Application;

public static class RoleServiceCollectionExtension
{
    public static IServiceCollection AddRoleModule<TOptions>(this IServiceCollection services) where TOptions : class
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        var sectionName = typeof(TOptions).GetField(SharedConstant.SectionNamePlaceHolder)
            ?.GetRawConstantValue()
            ?.ToString();

        services.AddValidatorsFromAssemblyContaining<IRoleMarker>(ServiceLifetime.Transient);

        services.AddOptions<TOptions>()
            .BindConfiguration(sectionName!)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }

    public static IServiceCollection AddRoleDatabaseModule(this IServiceCollection services)
    {
        return services;
    }
}
