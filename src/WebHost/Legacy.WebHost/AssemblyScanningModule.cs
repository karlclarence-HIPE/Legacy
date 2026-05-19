using System.Runtime.Loader;
using Autofac;
using Legacy.Shared;
using Legacy.Shared.Constants.Configurations;

namespace Legacy.WebHost;

public class AssemblyScanningModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var paths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, 
                WebHostConfigurationConstants.SearchPattern, 
                SearchOption.AllDirectories)
                .Where(cp => !cp.Contains(WebHostConfigurationConstants.ExcludedAssembly));

        var assemblies = paths.Select(path => 
            AssemblyLoadContext.Default.LoadFromAssemblyPath(path)).ToList();

        foreach(var assembly in assemblies)
        {
            var modules = assembly.GetTypes()
                .Where(t => typeof(ISystemModule).IsAssignableFrom(t) && !t.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<ISystemModule>();

            foreach (var module in modules)
            {
                module.Load(builder);
            }
        }
    }
}
