using Legacy.Shared.ErrorHandling;

namespace Legacy.Profile.Application.Services.Profile.Errors;

public static class ErrorCode
{
    private const string Module = "Profile";
    // General Error Codes
    public const string Internal = $"{Module}.Internal";
    public const string NotFound = $"{Module}.NotFound";
    public const string Invalid = $"{Module}.Invalid";
    public const string CreationError = $"{Module}.CreationError";
    public const string UpdatingError = $"{Module}.UpdatingError";

    // Required for update and delete
    public const string Id = $"{Module}.IdIsRequired";

    // Requiring for position title field error code
    public const string RoleNameIsRequired = $"{Module}.RoleNameIsRequired";
}

public static class ModuleError
{
    private static IReadOnlyDictionary<string, Error> ErrorDictionary => Errors.ToDictionary(k => k.Code, v => v);

    private static IEnumerable<Error> Errors =>
    [
        new Error(string.Empty, ErrorType.Validation, "Role Title is Required"), 

        new Error(string.Empty, ErrorType.Unexpected, "No Error Message Found for this specific Error Code."), 

        new Error(ErrorCode.Internal, ErrorType.Unexpected, "Something went wrong during processing the request."),
        
        new Error(ErrorCode.CreationError, ErrorType.Validation, "An error occured during profile creation process."), 

        new Error(ErrorCode.UpdatingError, ErrorType.Validation, "An error occured during profile update process"),
    
        new Error(ErrorCode.Id, ErrorType.Validation, "Id is Required."), 

        new Error(ErrorCode.RoleNameIsRequired, ErrorType.Validation, "Role name is Required.")
    ];

    public static Error RetrieveErrorByCode(string errorCode) => ErrorDictionary[errorCode];
}