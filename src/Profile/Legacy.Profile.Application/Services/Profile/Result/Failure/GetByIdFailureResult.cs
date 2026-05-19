using Legacy.Profile.Application.Services.Profile.Errors;
using Legacy.Shared.ErrorHandling;

namespace Legacy.Profile.Application.Services.Profile.Result.Failure;

public class GetByIdFailureResult : IFailureResult
{
    public List<Error> Errors { get; private set; }

    public bool HasErrors { get; init; } = true;

    private GetByIdFailureResult(Error error)
    {
        Errors = [error];
    }

    private GetByIdFailureResult(List<Error> errors) => Errors = errors;

    public static GetByIdFailureResult Throw(string errorCode) => new(ModuleError.RetrieveErrorByCode(errorCode));


    public static GetByIdFailureResult Throw(List<Error> errors) => new(errors);
}
