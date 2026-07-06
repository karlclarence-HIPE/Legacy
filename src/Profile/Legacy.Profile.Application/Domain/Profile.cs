using Legacy.Shared.Base;

namespace Legacy.Profile.Application.Domain;

public class Profile : AggregateRoot
{
    #region "Create Profile"

    private Profile(string name, string email, string password, Role role, DateTime createdAt, string? imageUrl, string? phoneNumber)
    {
        Name = name;
        Email = email;
        Password = password; 
        Role = role;
        CreatedAt = createdAt;
        ImageUrl = imageUrl;
        PhoneNumber = phoneNumber;
    }

    public static Profile Create(string name, string email, string password, Role role, DateTime createdAt, string? imageUrl = null, string? phoneNumber = null) =>
        new(name, email, password, role, createdAt, imageUrl, phoneNumber);

    public static Profile Update(int id, string name, string email, string password, Role role, DateTime createdAt, DateTime updatedAt, string? imageUrl = null, string? phoneNumber = null) =>
        new(id, name, email, password, role, createdAt, updatedAt, imageUrl, phoneNumber); 

    #endregion

    #region "Load Application"

    private Profile(int userId, string name, string email, string password, Role role, DateTime createdAt, DateTime updatedAt, string? imageUrl, string? phoneNumber)
    {
        UserId = userId;
        Name = name;
        Email = email; 
        Password = password;
        Role = role;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        ImageUrl = imageUrl;
        PhoneNumber = phoneNumber;
    }

    public static Profile Load(int userId, string name, string email, string password, Role role, DateTime createdAt, DateTime updatedAt, string? imageUrl, string? phoneNumber) =>
        new(userId, name, email, password, role, createdAt, updatedAt, imageUrl, phoneNumber);

    #endregion

    public string Name { get; private set; }

    public int UserId { get; private set; }

    public string Email { get; private set; }

    public string Password {  get; private set; }

    public string? ImageUrl { get; private set; } 

    public string? PhoneNumber { get; private set; }

    public Role Role { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    internal void UploadImage(string imageUrl)
    {
        ImageUrl = imageUrl;
    }
}
