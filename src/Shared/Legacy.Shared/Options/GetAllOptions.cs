namespace Legacy.Shared.Options;

public abstract class GetAllOptions
{
    public int Id { get; set; } 

    public DateRangeDto? DateRange { get; set; }

    public SortOrder SortOrder { get; set; }

    public string? SortField { get; set; }

    public int Page { get; set; }

    public int PageSize { get; set; }
}

public enum SortOrder
{
    Unsorted,
    Ascending,
    Descending,
}

public record DateRangeDto
{
    public string? DateFrom { get; init; }

    public string? DateTo { get; init; }
}