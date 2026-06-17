using Legacy.Role.Application.Common;
using Legacy.Role.Application.Common.Data;

namespace Legacy.Role.Application.Services.Role.Repository;

public interface IRoleRepository
{
    Task<bool> CreateAsync(Domain.Role entity, CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(Domain.Role entity, CancellationToken cancellationToken);

    Task<RoleDataModel> GetIdByAsync(int roleId, CancellationToken cancellationToken = default);
}
