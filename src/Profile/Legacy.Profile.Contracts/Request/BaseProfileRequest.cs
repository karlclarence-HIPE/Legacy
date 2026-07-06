using Microsoft.AspNetCore.Http;

namespace Legacy.Profile.Contracts.Request;

public class BaseProfileRequest
{
    public required string Name { get; init; }

    public required string Email { get; init; }

    public required string Password { get; init; }

    public string? PhoneNumber { get; init; }

    public IFormFile? ImageUrl { get; init; }

    public required RoleRequest Role { get; init; } 

    public required DateTime CreatedAt { get; init; }

}
