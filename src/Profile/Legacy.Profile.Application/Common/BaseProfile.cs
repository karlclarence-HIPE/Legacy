using Legacy.Profile.Application.Common.Mapping;

namespace Legacy.Profile.Application.Common;

public class BaseProfile
{
    public required string Name { get; init; }

    public required string Email { get; init; }

    public required string Password { get; init; }

    public required RoleModel Role { get; init; }

    public required DateTime CreatedAt { get; init; }

    public required DateTime UpdatedAt { get; init; }
}
