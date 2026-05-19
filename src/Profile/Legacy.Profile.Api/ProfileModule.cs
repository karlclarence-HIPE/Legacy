using Autofac;
using Autofac.Extensions.DependencyInjection;
using Legacy.Profile.Application;
using Legacy.Profile.Application.Configuration;
using Legacy.Shared;

namespace Legacy.Profile.Api;

public class ProfileModule : Module, ISystemModule
{
    public new void Load(ContainerBuilder builder)
    {
        var services = new ServiceCollection();
        builder.Populate(services);
    }
}
