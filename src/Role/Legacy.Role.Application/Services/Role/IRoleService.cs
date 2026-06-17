using Legacy.Role.Application.Common;
using Legacy.Role.Application.Services.Role.Result;
using Legacy.Role.Application.Services.Role.Result.Failure;
using Legacy.Shared.Utility;
namespace Legacy.Role.Application.Services.Role;

public interface IRoleService
{
    Task<Result<CreateRoleResult, CreateRoleFailureResult>> CreateAsync(CreateRole createRole,
        CancellationToken cancellationToken);

    Task<Result<UpdateRoleResult, UpdateRoleFailureResult>> UpdateAsync(UpdateRole updateRole,
        CancellationToken cancellationToken);

    Task<Result<Domain.Role, GetByIdRoleWithOptions>> GetByIdAsync(int userId, CancellationToken cancellationToken);
}
