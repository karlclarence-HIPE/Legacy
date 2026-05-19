using Dapper;
using Legacy.Profile.Application.Common.Data;
using Legacy.Profile.Application.Database;

namespace Legacy.Profile.Application.Services.Status.Repository;

public class ProfileStatusRepository : IProfileStatusRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public ProfileStatusRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory ?? throw new ArgumentNullException(nameof(dbConnectionFactory));
    }

    public async Task<ProfileStatusDataModel?> GetByName(string status,
    CancellationToken cancellationToken)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);

        var result = await connection.QuerySingleOrDefaultAsync<ProfileStatusDataModel>(
            new CommandDefinition(
                """SELECT user_id AS Id, Name FROM users WHERE Name LIKE @Status""",
                new { Status = string.Concat('%', status, '%') }, cancellationToken: cancellationToken
                )
            );

        return result;
    }
}
