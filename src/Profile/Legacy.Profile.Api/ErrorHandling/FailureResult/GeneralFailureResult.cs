using Legacy.Shared.ErrorHandling;

namespace Legacy.Profile.Api.ErrorHandling.FailureResult;

public class GeneralFailureResult : IFailureResult
{
    public List<Error> Errors { get; private set; }

    public bool HasErrors { get; init; } = true;

    private GeneralFailureResult(Error error)
    {
        Errors = [error];
    }

    private GeneralFailureResult(List<Error> errors) => Errors = errors;

    public static string Throw(string errorCode)
    {
        var failureResult = new GeneralFailureResult(ModuleError.RetrieveErrorByCode(errorCode));
        return failureResult.Errors.Single().Message;
    }

    public static GeneralFailureResult Throw(List<Error> errors) => new(errors);
}
