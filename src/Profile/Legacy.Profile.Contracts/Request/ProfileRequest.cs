namespace Legacy.Profile.Contracts.Request;

public class ProfileRequest
{
    public required string Name { get; set; }

    public required string UserId { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }
    
    public required RoleRequest Role { get; set; }

    public required DateTime CreatedAt { get; set; }

    public required DateTime UpdatedAt { get; set; }
}
