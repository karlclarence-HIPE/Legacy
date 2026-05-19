using System.Reflection;
using System.Runtime.Loader;
using Legacy.Shared;
using Legacy.Shared.Constants.Configurations;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Legacy.WebHost.Extensions;

public static class WebHostServiceCollectionExtension
{
    /// <summary>
    /// Extension method to add controllers to the service collection with a custom controller feature provider that considers all types as controllers.
    /// </summary>
    public static IServiceCollection AddRuntimeControllers(this IServiceCollection services)
    {
        var controllerPaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, 
                WebHostConfigurationConstants.SearchPattern, 
                SearchOption.AllDirectories)
                .Where(path => !Path.GetFileName(path).Equals(WebHostConfigurationConstants.ExcludedAssembly, StringComparison.OrdinalIgnoreCase));

        var assemblies = controllerPaths.Select(path => 
            AssemblyLoadContext.Default.LoadFromAssemblyPath(path)).ToList();

        foreach(var assembly in assemblies)
        {
            var modules = assembly.GetTypes()
                .Where(t => typeof(SystemController).IsAssignableFrom(t) && !t.IsAbstract);

            if (!modules.Any()) continue;

            services.AddControllers()
                .ConfigureApplicationPartManager(manager => 
                    manager.FeatureProviders.Add(new CustomControllerFeatureProvider()))
                .AddApplicationPart(assembly)
                .AddControllersAsServices();
        }

        return services;
    }
}


internal class CustomControllerFeatureProvider : ControllerFeatureProvider
{
    protected override bool IsController(TypeInfo typeInfo)
    {
        var isCustomController = !typeInfo.IsAbstract && typeof(SystemController).IsAssignableFrom(typeInfo);

        return isCustomController || base.IsController(typeInfo);
    }
}