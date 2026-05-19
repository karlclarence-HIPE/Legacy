namespace Legacy.Security.UserGroup.Shared.Common;

public class UserGroupModuleDbModel
{
    public required int Id { get; init; }

    public required string ModuleCode { get; init; }

    public required string ModuleDescription { get; init; }

    public required string ParentModuleName { get; init; }

    public required int UserGroupId { get; init; }

    public bool CanSave { get; init; }

    public bool CanCancel { get; init; }

    public bool CanApprove { get; init; }

    public bool CanReview { get; init; }

    public bool CanPrint { get; init; }

    public bool CanFileOnBehalf { get; init; }
}
