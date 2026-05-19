namespace Legacy.Security.UserGroup.Shared;

public interface IUserGroupModuleApi
{
    Task<int> GetParentIdAsync(int userGroupId, CancellationToken cancellationToken = default);
}
