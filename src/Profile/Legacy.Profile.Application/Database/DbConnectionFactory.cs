using Legacy.Profile.Application.Configuration;
using Npgsql;
using Microsoft.Extensions.Options;
using System.Data;

namespace Legacy.Profile.Application.Database;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateConnectionAsync(CancellationToken cancellationToken = default); 
}

public class DbConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;
    
    public DbConnectionFactory(IOptionsMonitor<ModuleConfigurationOptions> configurations) =>
        _connectionString = configurations.CurrentValue.ConnectionString;

    public DbConnectionFactory(string connectionString) => _connectionString = connectionString;

    public async Task<IDbConnection> CreateConnectionAsync(CancellationToken cancellationToken = default)
    {
        var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        return connection;
    }
}