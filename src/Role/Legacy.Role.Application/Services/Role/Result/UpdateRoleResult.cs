namespace Legacy.Role.Application.Services.Role.Result;

public class UpdateRoleResult
{
    public Domain.Role Role { get; set; }

    private UpdateRoleResult(Domain.Role role) => Role = role;

    public static UpdateRoleResult Success(Domain.Role role) => new(role);
}
