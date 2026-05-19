using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Legacy.Shared.Configuration;

public class ConfigurationSetup<TOption>(IConfiguration configuration) 
    : IConfigureOptions<TOption> where TOption : class
{
    public void Configure(TOption options)
    {
        var sectionName = typeof(TOption).GetField("SectionName")?.GetRawConstantValue()?.ToString();
        configuration.GetSection(sectionName!).Bind(options);
    }
}
