using Legacy.Profile.Application.Services.Profile.Result;
using Legacy.Profile.Application.Services.Profile.Result.Failure;
using Legacy.Shared.Utility;

namespace Legacy.Profile.Application.Common;

public interface IProfileService
{
    Task<Result<CreateProfileResult, CreateProfileFailureResult>> CreateAsync(CreateProfile createProfile, 
        CancellationToken cancellationToken);

    Task<Result<UpdateProfileResult, UpdateProfileFailureResult>> UpdateAsync(UpdateProfile updateProfile, 
        CancellationToken cancellationToken);

    Task<Result<Domain.Profile, GetByIdFailureResult>> GetByIdAsync(int userId, CancellationToken cancellationToken);
}
