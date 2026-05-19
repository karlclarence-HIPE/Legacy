using Autofac;

namespace Legacy.Shared;

public interface ISystemModule
{
    void Load(ContainerBuilder builder);
}
