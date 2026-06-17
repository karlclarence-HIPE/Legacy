using Dapper;
using Legacy.Role.Application.Common.Data;
using Legacy.Role.Application.Database;

namespace Legacy.Role.Application.Services.Status.Repository;

public class RoleStatusRepository : IRoleStatusRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public RoleStatusRepository(IDbConnectionFactory dbConnectionFactory)
    {
        this._dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<RoleStatusDataModel?> GetByName(string status, CancellationToken cancellationToken)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);

        var result = await connection.QuerySingleOrDefaultAsync<RoleStatusDataModel>(
            new CommandDefinition(
                    """SELECT role_id AS Id, Name FROM users WHERE Name LIKE @Status""",
                    new { Status = string.Concat('%', status, '%') }, cancellationToken: cancellationToken
                    )
            );

        return result;
    }
}
