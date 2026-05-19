using FluentValidation;
using Legacy.Profile.Application.Database;
using Legacy.Profile.Application.Services.Profile;
using Legacy.Profile.Application.Services.Profile.Repository;
using Legacy.Profile.Application.Common;
using Legacy.Shared.Constants;
using Legacy.Shared.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Legacy.Profile.Application.Factory;
using Legacy.Shared.Provider;
using Legacy.Profile.Application.Services.Status.Repository;


namespace Legacy.Profile.Application;

public static class ProfileServiceCollectionExtension
{
    public static IServiceCollection AddProfileModule<TOptions>(this IServiceCollection services) where TOptions : class
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        var sectionName = typeof(TOptions).GetField(SharedConstant.SectionNamePlaceHolder)
            ?.GetRawConstantValue()
            ?.ToString();

        services.AddValidatorsFromAssemblyContaining<IProfileMarker>(ServiceLifetime.Transient);

        services.AddOptions<TOptions>()
            .BindConfiguration(sectionName!)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddScoped<IProfileService, ProfileService>();
        services.AddScoped<IProfileRepository, ProfileRepository>();
        services.AddScoped<IProfileStatusRepository, ProfileStatusRepository>();
        services.AddScoped<IProfileFactory, ProfileFactory>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        return services;
    }

    public static IServiceCollection AddProfileDatabaseModule(this IServiceCollection services)
    {
        services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
        return services;
    }
}
