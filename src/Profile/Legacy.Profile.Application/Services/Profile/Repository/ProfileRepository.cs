using Dapper;
using Legacy.Authentication.Application.Common;
using Legacy.Profile.Application.Common.Data;
using Legacy.Profile.Application.Database;
using Legacy.Shared.Format;
using System.Data;
namespace Legacy.Profile.Application.Services.Profile.Repository;

public class ProfileRepository : IProfileRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public ProfileRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory ?? throw new ArgumentNullException(nameof(dbConnectionFactory));
    }

    public async Task<bool> CreateAsync(Domain.Profile entity, CancellationToken cancellationToken = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);

        using var transaction = connection.BeginTransaction();

        try
        {
            var recordId = await connection.QuerySingleAsync<int>(new CommandDefinition(
                """
                    INSERT INTO users
                        (name, email, password_hash, role_id, created_at)
                    VALUES (@Name, @Email, @Password, @Role_Id, @CreatedAt)
                    RETURNING id;
                """, new
                {
                    Name = entity.Name, 
                    Email = entity.Email,
                    Password = entity.Password,
                    Role_Id = entity.Role.RoleId, 
                    CreatedAt = entity.CreatedAt.ToString(Format.Date)
                }, 
                transaction: transaction, 
                cancellationToken: cancellationToken
             ));

            transaction.Commit();

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

    public async Task<bool> UpdateAsync(Domain.Profile entity, CancellationToken cancellationToken)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);

        using var transaction = connection.BeginTransaction();

        try
        {
            await connection.ExecuteAsync(new CommandDefinition(
                """
                    UPDATE 
                        users 
                    SET 
                        name = @Name, 
                        role_id = @RoleId, 
                        email = @Email, 
                        updated_at = @UpdatedAt
                    WHERE user_id = @UserId
                """, new 
                {
                    Name = entity.Name, 
                    RoleId = entity.Role.RoleId,   
                    Email = entity.Email, 
                    UpdatedAt = entity.UpdatedAt.ToString(Format.Date)
                }, 
                transaction: transaction, cancellationToken: cancellationToken));

            transaction.Commit();
            return true;
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

    public async Task<ProfileDataModel?> GetByIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);

        var sql = $"""
                   SELECT * 
                   FROM users AS u 
                        INNER JOIN roles r
                                ON u.RoleId = r.ID
                   WHERE (@UserId   = 0
                        OR u.UserId = @UserId)
                   """;

        return await connection.QueryFirstOrDefaultAsync<ProfileDataModel>(
            new CommandDefinition(
                sql, 
                new { UserId = userId }, 
                cancellationToken: cancellationToken
            )
        );
    }

    public async Task<ILookup<int, RoleDataModel>> RetrievalRoleAsync(IDbConnection connection, IEnumerable<int> ids, CancellationToken cancellationToken = default)
    {
        var sql = """
                  SELECT 
                    role_id as Id
                  FROM roles 
                  WHERE ID = @Id
                  """;
        var result = await connection.QueryAsync<RoleDataModel>(new CommandDefinition(sql, new { Id = ids },
            cancellationToken: cancellationToken));

        return result.ToLookup(s => s.RoleId);
    }

    public async Task ExecuteQueryAsync(IDbConnection connection, Dictionary<int, ProfileDataModel> profileDictionary, CancellationToken cancellationToken)
    {
        var roleRetrievalTask = RetrievalRoleAsync(connection, profileDictionary.Keys, cancellationToken);

        await Task.WhenAll(roleRetrievalTask);

        foreach (var id in profileDictionary.Keys)
        {
        }
    }


}
