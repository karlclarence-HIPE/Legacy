

using Legacy.Framework.Utility.Database;
using Legacy.Framework.Utility.Provider;
using Legacy.Shared.Constants;
using Legacy.Shared.Extensions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Legacy.Framework.Utility.Extensions;

public static class UtilityExtension
{
    public static IServiceCollection AddUtilities<TOptions>(this IServiceCollection services) where TOptions: class
    {
        return services;
    }
}
