using Legacy.Shared.Base;

namespace Legacy.Profile.Application.Domain;

public class Profile : AggregateRoot
{
    #region "Create Profile"

    private Profile(string name, string email, string password, Role role, DateTime createdAt)
    {
        Name = name;
        Email = email;
        Password = password; 
        Role = role;
        CreatedAt = createdAt;
    }

    public static Profile Create(string name, string email, string password, Role role, DateTime createdAt) =>
        new(name, email, password, role, createdAt);

    public static Profile Update(int id, string name, string email, string password, Role role, DateTime createdAt) =>
        new(id, name, email, password, role, createdAt); 

    #endregion

    #region "Load Application"

    private Profile(int userId, string name, string email, string password, Role role, DateTime updatedAt)
    {
        UserId = userId;
        Name = name;
        Email = email; 
        Password = password;
        Role = role;
        UpdatedAt = updatedAt;
    }

    public static Profile Load(int userId, string name, string email, string password, Role role, DateTime updatedAt) =>
        new(userId, name, email, password, role, updatedAt);

    #endregion

    public string Name { get; private set; }

    public int UserId { get; private set; }

    public string Email { get; private set; }

    public string Password {  get; private set; }

    public string ImageUrl { get; private set; } = string.Empty;

    public Role Role { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    internal void UploadImage(string imageUrl)
    {
        ImageUrl = imageUrl;
    }
}
