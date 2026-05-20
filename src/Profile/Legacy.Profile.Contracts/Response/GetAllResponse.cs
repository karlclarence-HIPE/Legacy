namespace Legacy.Profile.Contracts.Response;

public class GetAllResponse
{
    public required IEnumerable<ProfileResponse> Items { get; init; } = [];

    public required int Page { get; init; }

    public required int PageSize { get; init; }

    public required int PageCount { get; init; }

    public required int Total { get; init; }
}
