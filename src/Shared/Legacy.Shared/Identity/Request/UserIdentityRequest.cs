namespace Legacy.Shared.Identity.Request;

public class UserIdentityRequest
{
    public int Id { get; private set; }

    public string UserName { get; private set; }

    public string Email { get; private set; }

    public string Password { get; private set; }

    #region "Initialize"

    private UserIdentityRequest(string userName, string email, string password)
    {
        UserName = userName;
        Email = email;
        Password = password;
    }

    public static UserIdentityRequest Create(string userName, string email, string password)
    {
        return new UserIdentityRequest(userName, email, password);
    }

    #endregion
}
