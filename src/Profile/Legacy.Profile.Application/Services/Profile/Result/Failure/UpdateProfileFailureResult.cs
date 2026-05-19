using Legacy.Profile.Application.Services.Profile.Errors;
using Legacy.Shared.ErrorHandling;

namespace Legacy.Profile.Application.Services.Profile.Result.Failure;

public class UpdateProfileFailureResult : IFailureResult
{
    public List<Error> Errors { get; }

    public bool HasErrors { get; init; }

    private UpdateProfileFailureResult(Error error) => Errors = [error];

    private UpdateProfileFailureResult(List<Error> errors) => Errors = errors;

    public static UpdateProfileFailureResult Throw(string errorCode) => new(ModuleError.RetrieveErrorByCode(errorCode));

    public static UpdateProfileFailureResult Throw(List<Error> errors) => new(errors);
}
