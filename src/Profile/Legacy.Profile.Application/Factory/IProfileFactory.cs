using Legacy.Profile.Application.Common;
using Legacy.Profile.Application.Common.Data;

namespace Legacy.Profile.Application.Factory;

public interface IProfileFactory
{
    Task<Domain.Profile> CreateProfileAsync(CreateProfile createProfile);

    Domain.Profile CreateProfileAsync(ProfileDataModel data);

    Domain.Profile UpdateProfileAsync(UpdateProfile updateProfile);

    Domain.Profile UpdateProfileAsync(ProfileDataModel data);
}
