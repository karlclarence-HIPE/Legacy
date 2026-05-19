using Legacy.Profile.Application.Services.Profile.Errors;
using Legacy.Shared.ErrorHandling;

namespace Legacy.Profile.Application.Services.Profile.Result.Failure;

public class CreateProfileFailureResult : IFailureResult
{
    public List<Error> Errors { get; }

    public bool HasErrors { get; init; }

    private CreateProfileFailureResult(Error error) => Errors = [error];

    private CreateProfileFailureResult(List<Error> errors) => Errors = errors;

    public static CreateProfileFailureResult Throw(string errorCode) => new(ModuleError.RetrieveErrorByCode(errorCode));

    public static CreateProfileFailureResult Throw(List<Error> errors) => new(errors);
}
