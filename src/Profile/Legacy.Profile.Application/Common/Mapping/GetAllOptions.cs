using Legacy.Shared.Options;

namespace Legacy.Profile.Application.Common.Mapping;

public class GetAllOptions
{
    public required string? Name { get; init; }

    public string? CreatedAt { get; init; }

    public string? UpdatedAt { get; init; }

    public int RoleId { get; set; }

    public DateRangeDto? DateRange { get; set; }

    public required IEnumerable<int> RoleIds { get; set; } = [];

    public SortOrder? SortOrder { get; init; }

    public string? SortField { get; set; }

    public int Page { get; set; }

    public int PageSize { get; set; }
}

public record DateRangeDto
{
    public string? CreatedAt { get; init; }

    public string? UpdatedAt { get; init; }
}
