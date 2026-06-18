namespace Legacy.Profile.Application.Services.Profile.Result;

public class GetAllResult
{
    public IReadOnlyList<Domain.Profile> Items { get; private set; }

    public GetAllResult() => Items = [];

    public GetAllResult(IEnumerable<Domain.Profile> records) => Items = records.ToList();

    public static GetAllResult Success(IEnumerable<Domain.Profile> records) => new(records);

    public static GetAllResult Empty() => new();
}
