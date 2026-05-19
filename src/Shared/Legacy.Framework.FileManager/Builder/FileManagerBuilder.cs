using Microsoft.Extensions.DependencyInjection;

namespace Legacy.Framework.FileManager.Builder;

public class FileManagerBuilder(IServiceCollection services) : IFileManagerBuilder
{
    public IServiceCollection Services { get; } = services;
}
