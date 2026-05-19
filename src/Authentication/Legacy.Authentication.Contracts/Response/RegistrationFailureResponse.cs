namespace Legacy.Authentication.Contracts.Response;

public class RegistrationFailureResponse
{
    public required IEnumerable<ErrorResponse> Errors { get; set; }

    public bool HasErrors { get; set; } = true;
}
