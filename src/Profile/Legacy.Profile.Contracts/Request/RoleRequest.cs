namespace Legacy.Profile.Contracts.Request;

public abstract class RoleRequest
{
    public required int RoleId { get; set; }

    public required string RoleName { get; set; } = string.Empty;
}
