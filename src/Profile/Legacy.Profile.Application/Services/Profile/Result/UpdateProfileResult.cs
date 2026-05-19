namespace Legacy.Profile.Application.Services.Profile.Result;

public class UpdateProfileResult
{
    public Domain.Profile Profile { get; set; }

    public UpdateProfileResult(Domain.Profile profile) => Profile = profile;

    public static UpdateProfileResult Success(Domain.Profile profile) => new(profile);
}
