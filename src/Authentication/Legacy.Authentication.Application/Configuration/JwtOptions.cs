namespace Legacy.Authentication.Application.Configuration;

public class JwtOptions
{
    public const string SectionName = "Authentication:JwtOptions"; 

    public required string Issuer { get; init; }

    public required string Audience { get; init; }

    public required string SigningKey { get; init; }    

    public required int ExpirationTime { get; init; }

    public required int RefreshDuration { get; init; }
}
