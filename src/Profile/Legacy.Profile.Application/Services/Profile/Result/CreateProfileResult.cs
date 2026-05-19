namespace Legacy.Profile.Application.Services.Profile.Result;

public class CreateProfileResult
{
    public Domain.Profile Profile { get; set; }

    private CreateProfileResult(Domain.Profile profile) => Profile = profile;

    public static CreateProfileResult Success(Domain.Profile profile) => new(profile);
}
