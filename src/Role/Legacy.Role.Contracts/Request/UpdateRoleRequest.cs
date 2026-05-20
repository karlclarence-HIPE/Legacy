namespace Legacy.Role.Contracts.Request;

public class UpdateRoleRequest : BaseRoleRequest
{
    public required int RoleId { get; set; }
}
