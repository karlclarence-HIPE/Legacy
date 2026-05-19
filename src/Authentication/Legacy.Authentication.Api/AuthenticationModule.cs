using Autofac;
using Autofac.Extensions.DependencyInjection;
using Legacy.Authentication.Application;
using Legacy.Authentication.Application.Configuration;
using Legacy.Shared;

namespace Legacy.Authentication.Api;

public class AuthenticationModule : Module, ISystemModule
{
    public new void Load(ContainerBuilder builder)
    {
        var services = new ServiceCollection(); 

    }
}
