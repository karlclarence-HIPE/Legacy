namespace Legacy.Profile.Contracts.Request;

public class RoleRequest
{
    public required int RoleId { get; init; }

    public required string RoleName { get; init; } = string.Empty;
}
