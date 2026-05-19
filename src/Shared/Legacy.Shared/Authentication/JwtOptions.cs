namespace Legacy.Shared.Authentication;

public record JwtOptions(
    string Issuer,
    string Audience,
    string SigningKey,
    int ExpirationTime,
    int RefreshDuration = 0
);