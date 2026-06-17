using Legacy.Role.Application.Common.Data;
namespace Legacy.Role.Application.Services.Status.Repository;

public interface IRoleStatusRepository
{
    Task<RoleStatusDataModel?> GetByName(string status, CancellationToken cancellationToken);

}
