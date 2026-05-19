using Legacy.Authentication.Application.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace Legacy.Authentication.Application.Database;

public interface IUnitOfWork : IDisposable
{
    void Begin();

    void Close();

    void SaveChanges();
}

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateConnectionAsync(CancellationToken cancellationToken = default);
}

public class DbConnectionFactory(IOptionsMonitor<AuthenticationModuleConfiguration> configuration): IDbConnectionFactory 
{
    public async Task<IDbConnection> CreateConnectionAsync(CancellationToken cancellationToken = default)
    {
        var connection = new SqlConnection(configuration.CurrentValue.ConnectionString);
        await connection.OpenAsync(cancellationToken);

        return connection;
    }
}
