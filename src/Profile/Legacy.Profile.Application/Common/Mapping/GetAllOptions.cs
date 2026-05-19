namespace Legacy.Profile.Application.Common.Mapping;

public class GetAllOptions
{
    public int UserId { get; set; }

    public required string? Name { get; init; }

    public required string? Email { get; init; }

    public required string? Password { get; init; }

    public string? CreatedAt { get; init; }

    public string? UpdatedAt { get; init; }

    public int RoleId { get; set; }
    
    public int Page { get; set; }

    public int PageSize { get; set; }
}

public enum SortOrder
{
    Unsorted, Ascending, Descending
}

public record DateRangeDto
{
    public string? CreatedAt { get; init; }

    public string? UpdatedAt { get; init; }
}
