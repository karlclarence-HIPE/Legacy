using Legacy.Shared.ErrorHandling;

namespace Legacy.Profile.Api.ErrorHandling.FailureResult;

public class GetByIdFailureResult : IFailureResult
{
    public List<Error> Errors { get; private set; }

    public bool HasErrors { get; init; } = true;

    private GetByIdFailureResult(Error error)
    {
        Errors = [error];
    }

    private GetByIdFailureResult(List<Error> errors) => Errors = errors;

    public static string Throw(string errorCode)
    {
        var failureResult = new GetByIdFailureResult(ModuleError.RetrieveErrorByCode(errorCode));
        return failureResult.Errors.Single().Message;
    }

    public static GetByIdFailureResult Throw(List<Error> errors) => new(errors);
}
