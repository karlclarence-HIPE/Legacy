namespace Legacy.Authentication.Application.Services.Common;

public class AuthenticationResult
{
    public string AccessToken { get; private set; }

    public string RefreshToken { get; private set; }

    public DateTime ExpiryDate { get; private set; }

    private AuthenticationResult(string accessToken, string refreshToken, DateTime expiryDate)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        ExpiryDate = expiryDate;
    }

    public static AuthenticationResult Create(string accessToken, string refreshToken, DateTime expiryDate)
    {
        return new AuthenticationResult(accessToken, refreshToken, expiryDate);
    }
}
