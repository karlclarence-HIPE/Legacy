

using FluentValidation;
using Legacy.Authentication.Application.Common;
using Legacy.Profile.Application.Common;
using Legacy.Profile.Application.Factory;
using Legacy.Profile.Application.Services.Profile.Errors;
using Legacy.Profile.Application.Services.Profile.Repository;
using Legacy.Profile.Application.Services.Profile.Result;
using Legacy.Profile.Application.Services.Profile.Result.Failure;
using Legacy.Profile.Application.Services.Status.Repository;
using Legacy.Shared.Utility;

namespace Legacy.Profile.Application.Services.Profile;

public class ProfileService : IProfileService
{
    private readonly IProfileRepository _profileRepository;
    private readonly IValidator<CreateProfile> _createRecordValidator;
    private readonly IValidator<UpdateProfile> _updateRecordValidator;
    private readonly IProfileFactory _profileFactory;
    private readonly IProfileStatusRepository _profileStatusRepository;

    public ProfileService(IProfileRepository profileRepository, 
        IValidator<CreateProfile> createRecordValidator,
        IValidator<UpdateProfile> updateRecordValidator, 
        IProfileFactory profileFactory, 
        IProfileStatusRepository profileStatusRepository)
    {
        _profileRepository = profileRepository;
        _createRecordValidator = createRecordValidator;
        _updateRecordValidator = updateRecordValidator;
        _profileFactory = profileFactory;
        _profileStatusRepository = profileStatusRepository;
    }

    public async Task<Result<CreateProfileResult, CreateProfileFailureResult>> CreateAsync(
        CreateProfile createProfile, CancellationToken cancellationToken)
    {
        var validationResult = _createRecordValidator.Validate(createProfile);

        if (!validationResult.IsValid)
        {
            return CreateProfileFailureResult.Throw(validationResult.Errors
                .Select(error => ModuleError.RetrieveErrorByCode(error.ErrorMessage)).ToList());
        }

        var profile = _profileFactory.CreateProfileAsync(createProfile);

        var isCreated = await _profileRepository.CreateAsync(profile, cancellationToken);

        if (isCreated) return CreateProfileFailureResult.Throw(ErrorCode.CreationError);

        return CreateProfileResult.Success(profile);
    }

    public async Task<Result<UpdateProfileResult, UpdateProfileFailureResult>> UpdateAsync(UpdateProfile updateProfile, 
        CancellationToken cancellationToken)
    {
        var validationResult = _updateRecordValidator.Validate(updateProfile);

        if (!validationResult.IsValid)
        {
            return UpdateProfileFailureResult.Throw(validationResult.Errors
                .Select(error => ModuleError.RetrieveErrorByCode(error.ErrorMessage)).ToList());
        }

        var profile = _profileFactory.UpdateProfileAsync(updateProfile);
        var isCreated = await _profileRepository.UpdateAsync(profile, cancellationToken);

        if (!isCreated) return UpdateProfileFailureResult.Throw(ErrorCode.UpdatingError); 

        return UpdateProfileResult.Success(profile);    

    }

    public async Task<Result<Domain.Profile, GetByIdFailureResult>> GetByIdAsync(int userId, CancellationToken cancellationToken)
    {
        var profile = await _profileRepository.GetByIdAsync(userId, cancellationToken);

        return profile is null
            ? GetByIdFailureResult.Throw(ErrorCode.NotFound)
            : GetByIdResult.Success(_profileFactory.CreateProfileAsync(profile));
    }

}
