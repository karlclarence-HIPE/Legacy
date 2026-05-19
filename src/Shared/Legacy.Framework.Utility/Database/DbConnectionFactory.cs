using System.Data;
using Legacy.Framework.Utility.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Legacy.Framework.Utility.Database;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateConnectionAsync(CancellationToken cancellationToken = default);
}

public class DbConnectionFactory(IOptionsMonitor<UtilityConfigurationOptions> configuraitons) : IDbConnectionFactory
{
    public async Task<IDbConnection> CreateConnectionAsync(CancellationToken cancellationToken = default)
    {
        var connection = new SqlConnection(configuraitons.CurrentValue.ConnectionString);
        await connection.OpenAsync(cancellationToken);

        return connection;
    }
}
