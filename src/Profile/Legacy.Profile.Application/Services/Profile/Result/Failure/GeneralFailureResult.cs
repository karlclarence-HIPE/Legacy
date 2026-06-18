using Legacy.Shared.ErrorHandling;
using Legacy.Profile.Application.Services.Profile.Errors;

namespace Legacy.Profile.Application.Services.Profile.Result.Failure;

public class GeneralFailureResult
{
    public List<Error> Errors { get; private set; }

    public bool HasErrors { get; init; } = true;

    private GeneralFailureResult(Error error)
    {
        Errors = [error]; 
    }

    private GeneralFailureResult(List<Error> errors) => Errors = errors;

    public static GeneralFailureResult Throw(string errorCode) => new (ModuleError.RetrieveErrorByCode(errorCode));

    public static GeneralFailureResult Throw(List<Error> errors) => new(errors);
}
