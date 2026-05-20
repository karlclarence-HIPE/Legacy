namespace Legacy.Profile.Contracts.Response;

public class BaseProfileResponse
{
    public required string Name { get; init; }

    public required int UserId { get; init; }

    public required string Email { get; init; }

    public required string Password { get; init; }

    public required RoleResponse Role { get; init; }

    public required DateTime CreateAt { get; init; }

    public required DateTime UpdatedAt { get; init; }
}
