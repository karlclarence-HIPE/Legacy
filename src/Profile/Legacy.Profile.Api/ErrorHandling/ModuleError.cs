using Legacy.Shared.ErrorHandling;

namespace Legacy.Profile.Api.ErrorHandling; 

public static class ErrorCode
{
    public const string Filter = "Profile.Filter";
    public const string LackOfCredential = "Profile.LackOfCredential";
}

public static class ModuleError
{
    private static IReadOnlyDictionary<string, Error> ErrorDictionary = new Dictionary<string, Error>();

    public static Error RetrieveErrorByCode(string errorCode) => ErrorDictionary[errorCode];
}
