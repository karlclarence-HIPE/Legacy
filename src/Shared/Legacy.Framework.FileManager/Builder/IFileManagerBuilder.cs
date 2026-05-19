using Microsoft.Extensions.DependencyInjection;

namespace Legacy.Framework.FileManager.Builder;

public interface IFileManagerBuilder
{
    IServiceCollection Services { get; }
}
