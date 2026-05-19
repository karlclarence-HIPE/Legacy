using System.Text;
using System.Security.Cryptography;

namespace Legacy.Authentication.Application.Security;

public static class PasswordHasher
{
    private const int KeySize = 64;
    private const int Iterations = 100_000;
    private static readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA512;

    public static string HashPassword(string password, byte[] salt)
    {
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            Iterations,
            HashAlgorithm,
            KeySize
        );
        return Convert.ToBase64String(hash);
    }

    public static bool VerifyPassword(string password, string hashedPassword, byte[] salt)
    {
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, 
            salt, 
            Iterations, 
            HashAlgorithm, 
            KeySize);

        return CryptographicOperations.FixedTimeEquals(
            hashToCompare, 
            Convert.FromHexString(hashedPassword)
        );
    }
}
