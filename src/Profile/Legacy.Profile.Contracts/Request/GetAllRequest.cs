using Legacy.Shared.Request;

namespace Legacy.Profile.Contracts.Request;

public class GetAllRequest : PagedRequest
{
    public required string? Name { get; init; }

    public required string? Email { get; init; }

    public required string? DateField { get; init; }
    
    public required string? DateFrom { get; init; }

    public required string? DateTo { get; init; }

    public required IEnumerable<int>? RoleIds { get; init; }

    public required string? CreatedAt { get; init; }

    public required string? UpdateAt { get; init; }

    public required string? SortBy { get; init; }
}
