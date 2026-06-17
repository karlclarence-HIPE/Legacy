using Dapper;
using Legacy.Role.Application.Common.Data;
using Legacy.Role.Application.Database;
using Legacy.Shared.Format;
using System.Data;

namespace Legacy.Role.Application.Services.Role.Repository;

public class RoleRepository : IRoleRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public RoleRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory ?? throw new ArgumentNullException(nameof(dbConnectionFactory));
    }

    public async Task<bool> CreateAsync(Domain.Role entity, CancellationToken cancellationToken)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);

        using var transaction = connection.BeginTransaction();
        try
        {
            var recordId = await connection.QuerySingleAsync<int>(new CommandDefinition(
                """
                    INSERT INTO roles
                        (name, created_at)
                    VALUES (@Name, CAST(@CreatedAt AS TIMESTAMP))
                    RETURNING role_id;
                """, new
                {
                    Name = entity.RoleName, 
                },
                transaction: transaction,
                cancellationToken: cancellationToken
             ));

            return recordId > 0; 
        }
        catch (System.Exception)
        {
            transaction.Rollback();
            throw;
        }
        finally
        {
            connection.Close();
        }
    }

    public async Task<bool> UpdateAsync(Domain.Role entity, CancellationToken cancellationToken)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);

        using var transaction = connection.BeginTransaction();

        try
        {
            await connection.ExecuteAsync(new CommandDefinition(
                """
                    UPDATE 
                        roles
                    SET 
                        name = @RoleName,
                    WHERE role_id = @RoleId
                """, new
                {
                    RoleName = entity.RoleName
                }, 
                transaction: transaction, cancellationToken: cancellationToken
               ));

            transaction.Commit();
            return true;
        }
        catch(System.Exception)
        {
            transaction.Rollback();
            throw;
        }
        finally
        {
            connection.Close();
        }
    }

    public async Task<RoleDataModel> GetIdByAsync(int roleId, CancellationToken cancellationToken)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);

        var sql = $"""
                    SELECT * 
                    FROM roles AS r 
                    WHERE (@RoleId  = 0
                        OR Id = @RoleId)
                   """;

        return await connection.QueryFirstOrDefaultAsync<RoleDataModel>(
            new CommandDefinition(
                sql,
                new { RoleId = roleId },
                cancellationToken: cancellationToken
            )
        );
    }

}
