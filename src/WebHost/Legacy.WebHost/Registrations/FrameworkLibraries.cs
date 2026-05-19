using Legacy.Framework.FileManager.Configuration;
using Legacy.Framework.FileManager.Extensions;
using Legacy.Framework.Utility.Configuration;
using Legacy.Framework.Utility.Extensions;

namespace Legacy.WebHost.Registrations;

public static class FrameworkLibraries
{
    public static IServiceCollection AddSharedFrameworkLibraries(this IServiceCollection services)
    {
        services.AddFileManager<FileManagerConfigurationOption>();
        services.AddUtilities<UtilityConfigurationOptions>();

        return services;
    }
}
