namespace Legacy.Authentication.Application.Configuration;

public class AuthenticationModuleConfiguration
{
    public const string SectionName = "Authentication";

    /// <summary>
    /// Connection String
    /// </summary>
    public required string ConnectionString { get; set; }

    /// <summary>
    /// User Group Module Code
    /// </summary>
    public required string ModuleCode { get; set; }

    /// <summary>
    /// Configuration for JWT (JSON Web Token) authentication, including issuer, audience, signing key, expiration time, and refresh duration.
    /// </summary>
    public required JwtOptions JwtOptions { get; set; }

    /// <summary>
    /// Specifies the allowed origins for cross-origin requests, enabling CORS (Cross-Origin Resource Sharing) to control which domains can access the authentication services.
    /// </summary>
    public IEnumerable<string> AllowedOriginis { get; init; } = [];
}
