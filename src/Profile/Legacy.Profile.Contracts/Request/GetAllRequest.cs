using Legacy.Shared.Request;

namespace Legacy.Profile.Contracts.Request;

public class GetAllRequest : PagedRequest
{
    public required string? Name { get; init; }

    public required IEnumerable<int>? RoleIds { get; init; }

    public required string? CreatedAt { get; init; }

    public required string? UpdateAt { get; init; }

}
