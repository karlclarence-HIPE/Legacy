using Legacy.Shared.Base;

namespace Legacy.Profile.Application.Domain;

public class Role : Entity
{
    public int RoleId { get; private set; }

    public string RoleName { get; private set; }

    #region "Create"
    
    private Role(int roleId, string roleName)
    {
        if (roleId == 0)
        {
            throw new ArgumentException(nameof(roleId), "Role cannot be 0");
        }

        if (string.IsNullOrWhiteSpace(roleName))
            throw new ArgumentNullException(nameof(roleName), "Invalid role name");

        RoleId = roleId;
        RoleName = roleName;
    }

    //public static Role Record(int roleId, string roleName) => new(roleId, roleName);

    #endregion

    public static Role Create(int roleId, string roleName) => new(roleId, roleName);

    public static Role Update(int roleId, string roleName) => new(roleId, roleName);


    //protected override bool EqualsCore(Role other) => RoleId == other.RoleId && RoleName == other.RoleName;

    //protected override int GetHashCodeCore() => HashCode.Combine(RoleId, RoleName);
}
