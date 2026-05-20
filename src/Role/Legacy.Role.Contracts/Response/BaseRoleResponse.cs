namespace Legacy.Role.Contracts.Response;

public class BaseRoleResponse
{
    public required int RoleId { get; init; }

    public required string RoleName { get; init; }
}
