namespace Legacy.Security.Authorization.Policies;

public abstract class ModulePolicy
{
    public const string CreateUpdateModuleAccessPolicy = "CreateUpdateModuleAccessPolicy"; 
    public const string CancelModuleAccessPolicy = "CancelModuleAccessPolicy";
    public const string FilingOnBehalfModuleAccessPolicy = "FilingOnBehalfModuleAccessPolicy";
    public const string AdminOnlyPolicy = "AdminOnlyPolicy";
}
