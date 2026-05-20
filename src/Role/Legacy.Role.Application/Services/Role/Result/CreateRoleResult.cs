namespace Legacy.Role.Application.Services.Role.Result;

public class CreateRoleResult
{
    public Domain.Role Role { get; set; }

	private CreateRoleResult(Domain.Role role) => Role = role;

    public static CreateRoleResult Success(Domain.Role role) => new(role);
}
