using Legacy.Role.Application.Services.Role.Errors;
using Legacy.Shared.ErrorHandling;

namespace Legacy.Role.Application.Services.Role.Result.Failure;

public class UpdateRoleFailureResult : IFailureResult
{
    public List<Error> Errors { get; }

    public bool HasErrors { get; init; }
    
    private UpdateRoleFailureResult(Error error) => Errors = [error];
    
    private UpdateRoleFailureResult(List<Error> errors) => Errors = errors;
    
    public static UpdateRoleFailureResult Throw(string errorCode) => new(ModuleError.RetrieveErrorByCode(errorCode));
    
    public static UpdateRoleFailureResult Throw(List<Error> errors) => new(errors);
}
