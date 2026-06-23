using Legacy.Profile.Application.Services.Profile.Result;
using Legacy.Profile.Application.Services.Profile.Result.Failure;
using GetAllOptions = Legacy.Profile.Application.Common.Mapping.GetAllOptions;

using Legacy.Shared.Utility;

namespace Legacy.Profile.Application.Common;

public interface IProfileService
{
    Task<Result<CreateProfileResult, CreateProfileFailureResult>> CreateAsync(CreateProfile createProfile, 
        CancellationToken cancellationToken);

    Task<Result<UpdateProfileResult, UpdateProfileFailureResult>> UpdateAsync(UpdateProfile updateProfile, 
        CancellationToken cancellationToken);

    Task<Result<GetAllResult, GeneralFailureResult>> GetAllAsync(GetAllOptions options, CancellationToken cancellationToken);

    Task<Result<Domain.Profile, GetByIdFailureResult>> GetByIdAsync(GetByIdUserWithOptions options, CancellationToken cancellationToken);

    Task<int> GetRecordCountAsync(GetAllOptions options, CancellationToken cancellationToken = default);
}
