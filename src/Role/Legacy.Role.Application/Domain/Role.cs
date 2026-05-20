using Legacy.Shared.Base;
using System.Runtime;

namespace Legacy.Role.Application.Domain;

public class Role : AggregateRoot
{
    #region "Create Role"

    private Role(string roleName)
    {
        RoleName = roleName;
    }

    private Role(int roleId, string roleName)
    {
        RoleId = roleId; 
        RoleName = roleName;
    }

    public static Role Create(string name) =>
        new(name);

    public static Role Update(int id, string name) =>
        new(id, name);

    public static Role Load(int userId, string name) =>
        new(userId, name);

    #endregion

    public int RoleId { get; private set; }

    public string RoleName { get; private set; }
}
