
namespace Legacy.Authentication.Application.Services.Token.Model;

public class AuthenticationToken
{
    public Guid Guid { get; private set; }

    public int UserId { get; private set; } 

    public string JwtId { get; private set; }

    public string RefreshToken { get; private set; }

    public DateTime CreationDate { get; private set; }

    public DateTime ExpiryDate { get; private set; }

    public bool IsUsed { get; private set; }

    public bool IsRevoked { get; private set; }

    private AuthenticationToken(int userId, string jwtId, string refreshToken, DateTime expiryDate)
    {
        Guid = Guid.NewGuid();
        UserId = userId;
        JwtId = jwtId;
        RefreshToken = refreshToken;
        CreationDate = DateTime.UtcNow;
        ExpiryDate = expiryDate;
        IsRevoked = false;
        IsUsed = false;
    }

    private AuthenticationToken(Guid guid, int userId, string jwtId, string refreshToken, DateTime creationDate, 
        DateTime expiryDate, bool isUsed, bool isRevoked) 
    {
        Guid = guid;
        UserId = userId;
        JwtId = jwtId;
        RefreshToken = refreshToken;         
        CreationDate = creationDate;
        IsUsed = isUsed;
        IsRevoked = isRevoked;
    }

    public static AuthenticationToken Store(int userId, string jwtId, string refreshToken, DateTime expiryDate)
    {
        return new AuthenticationToken(userId, jwtId, refreshToken, expiryDate);
    }

    public static AuthenticationToken Create(Guid guid, int userId, string jwtId, string refreshToken, DateTime creationDate, 
        DateTime expiryDate, bool isUsed, bool isRevoked) 
    {
        return new AuthenticationToken(guid, userId, jwtId, refreshToken, creationDate, expiryDate, isUsed, isRevoked);
    }

    public AuthenticationToken MarkAsUsed(bool isUsed)
    {
        return new AuthenticationToken(Guid, UserId, JwtId, RefreshToken, CreationDate, ExpiryDate, isUsed, IsRevoked);
    }
}
