using Legacy.Shared.ErrorHandling;

namespace Legacy.Authentication.Application.Services.Common;

public class RegistrationFailureResult
{
    public List<Error> Errors { get; private set; }

    public bool HasErrors { get; private set; }

    private RegistrationFailureResult(Error error)
    {
        Errors = [error];
    }

    private RegistrationFailureResult(List<Error> errors)
    {
        Errors = errors;
    }

    public static RegistrationFailureResult Throw(List<Error> errors)
    {
        return new RegistrationFailureResult(errors);
    }
}
