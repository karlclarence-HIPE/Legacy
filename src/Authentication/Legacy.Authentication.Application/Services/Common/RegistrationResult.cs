namespace Legacy.Authentication.Application.Services.Common;

public class RegistrationResult
{
    public string Message { get; private set; }

    private RegistrationResult(string message)
    {
        Message = message;
    }

    public static RegistrationResult Create(string message)
    {
        return new RegistrationResult(message);
    }
}
