namespace Legacy.Profile.Application.Common.Data;

public class ProfileDataModel
{
    public required string Name { get; init; }

    public required int UserId { get; init; }

    public required string Email { get; init; }

    public required string Password { get; init; }

    public required RoleDataModel Role { get; init; }

    public required DateTime Created_at { get; init; }

    public required DateTime updated_at { get; init; }  
}
