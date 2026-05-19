using FluentValidation;
using Legacy.Framework.FileManager.Builder;
using Legacy.Framework.FileManager.Configuration;
using Legacy.Shared.Configuration;
using Legacy.Shared.Constants;
using Legacy.Shared.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Legacy.Framework.FileManager.Extensions;

public static class FileManagerServiceCollectionExtension
{
    public static IServiceCollection AddFileManager<TOptions>(this IServiceCollection services) where TOptions : class
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        var sectionName = typeof(TOptions).GetField(SharedConstant.SectionNamePlaceHolder)?.GetRawConstantValue()?.ToString();

        services.AddOptionsWithValidateOnStart<TOptions>()
            .BindConfiguration(sectionName!)
            .ValidateFluentValidation();

        services.AddSingleton<IFileManager, FileManager>();
        return services;
    }
}
