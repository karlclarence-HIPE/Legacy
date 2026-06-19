using Legacy.Shared.Routing;

namespace Legacy.Profile.Api.Routing;

public static class ApiRoute
{
    private const string ModuleBaseRoute = "legacy";

    private const string Base = $"{ApiBaseRoute.ApiBase}/{ModuleBaseRoute}/profile";

    public const string GetAll = Base;

    public const string Get = $"{Base}/{{id}}";

    public const string Create = $"{Base}/profile-form";

    public const string Update = $"{Base}/profile-update-form";
}
