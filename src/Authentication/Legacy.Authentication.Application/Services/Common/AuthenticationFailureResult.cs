namespace Legacy.Authentication.Application.Services.Common;

public class AuthenticationFailureResult
{
    public string AccessToken { get; private set; }

    public string RefreshToken { get; private set; }    

    public DateTime ExpiryDate { get; private set; }

    private AuthenticationFailureResult(string accessToken, string refreshToken, DateTime expiryDate)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        ExpiryDate = expiryDate;
    }

    public static AuthenticationFailureResult Create(string accessToken, string refreshToken, DateTime expiryDate)
    {
        return new AuthenticationFailureResult(accessToken, refreshToken, expiryDate);
    }
}
