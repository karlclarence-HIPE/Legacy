using Dapper;
using Legacy.Shared.Options;
using Legacy.Authentication.Application.Common;
using Legacy.Profile.Application.Common.Data;
using Legacy.Profile.Application.Common.Mapping;
using Legacy.Profile.Application.Database;
using Legacy.Shared.Format;
using System.Data;
using GetAllOptions = Legacy.Profile.Application.Common.Mapping.GetAllOptions;

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
                        (name, email, password_hash, role_id, created_at, image_url)
                    VALUES (@Name, @Email, @Password, @Role_Id, CAST(@CreatedAt AS TIMESTAMP), @ImageUrl)
                    RETURNING user_id;
                """, new
                {
                    Name = entity.Name, 
                    Email = entity.Email,
                    Password = entity.Password,
                    Role_Id = entity.Role.RoleId, 
                    CreatedAt = entity.CreatedAt.ToString(Format.Date),
                    ImageUrl = entity.ImageUrl
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
                   SELECT  u.user_id AS UserId, 
                           u.name AS Name, 
                           u.email AS Email, 
                           u.password_hash as Password, 
                           u.image_url AS ImageUrl, 
                           r.role_id as RoleId, 
                           r.role_name as RoleName, 
                           u.created_at AS Created_at, 
                           u.updated_at As Updated_at
                   FROM users AS u 
                        INNER JOIN roles r
                                ON u.role_id = r.role_id
                   WHERE (@UserId   = 0
                        OR u.user_id = @UserId)
                   """;

        var profileDirectory = new Dictionary<int, ProfileDataModel>();

        //await connection.QueryAsync<ProfileDataModel, RoleDataModel>(sql, (profile, role) =>
        //{
        //    if (!profileDirectory.TryGetValue(role.RoleId, out var existingProfile))
        //        profileDirectory.Add(role.RoleId, profile);

        //    return profile;
        //}, splitOn: "RoleId", param: new {

        //});

        await connection.QueryAsync<ProfileDataModel, RoleDataModel, ProfileDataModel>(sql, (profile, role) =>
        {
            if (!profileDirectory.TryGetValue(profile.UserId, out var existingProfile))
            {
                profileDirectory.Add(profile.UserId, profile);
            }
            return profile;

        }, splitOn: "RoleId", param: new { UserId = userId });

        return profileDirectory.SingleOrDefault().Value;
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

    public async Task<int> GetRecordCountAsync(GetAllOptions options, CancellationToken cancellationToken)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);
        var dateFilterClause = string.Empty;

        var sql = $"""
                    SELECT COUNT(DISTINCT a.ID)
                    FROM tVacancy v 
                      {dateFilterClause}  
                    """;
        var result = await connection.QuerySingleAsync(sql, cancellationToken);
        return result;
    }

    public async Task<IDictionary<int, ProfileDataModel>> GetAllAsync(GetAllOptions options, CancellationToken cancellationToken = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);

        var orderClause = "ORDER BY ID"; 

        if (options.SortField is not null)
        {
            orderClause =
                $"ORDER BY {options.SortField} {( options.SortOrder == SortOrder.Ascending ? "ASC" : "DESC" )}";
        }

        var dateFilterClause = string.Empty;

        var offset = string.Empty;

        if (options.Page > 0)
        {
            offset = $"""
                       OFFSET @PageOffset ROWS FETCH NEXT @PageSize ROWS ONLY
                      """;
        }

        var sql = $"""
                    SELECT * FROM users
                    """;
        var profilesDictionary = new Dictionary<int, ProfileDataModel>();

        return profilesDictionary;
    }

    public async Task<bool> ValidateIfExistAsync(string parameter, CancellationToken cancellationToken = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);

        var result = await connection.QuerySingleAsync<int>(new CommandDefinition(
            """
                SELECT COUNT(ID) AS InstanceNo 
                    FROM users
                WHERE Email LIKE CONCAT('%', @Parameter, '%') 
                OR Name LIKE CONCAT('%', @Parameter, '%') 
                OR RoleName LIKE CONCAT('%', @Parameter, '%') 
            """, new
            {
                Parameter = parameter
            }, cancellationToken: cancellationToken));

        return result > 0;
    }
}
