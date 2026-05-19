using Legacy.Framework.FileManager;
using Legacy.Profile.Application.Common;
using Legacy.Profile.Application.Common.Data;
using Legacy.Profile.Application.Configuration;
using Legacy.Profile.Application.Domain;
using Legacy.Shared.Provider;
using Legacy.Shared.Utility;
using Microsoft.Extensions.Options;

namespace Legacy.Profile.Application.Factory;

public class ProfileFactory : IProfileFactory
{
    private readonly IDateTimeProvider _dateTimeProvider; 
    private readonly IFileManager _fileManager;
    private readonly IOptionsMonitor<ModuleConfigurationOptions> _optionsMonitor;
    
    public ProfileFactory(IDateTimeProvider dateTimeProvider, IFileManager fileManager, 
        IOptionsMonitor<ModuleConfigurationOptions> confiigurations)
    {
        this._dateTimeProvider = dateTimeProvider;
        this._fileManager = fileManager;
        this._optionsMonitor = confiigurations;
    }

    public Domain.Profile CreateProfileAsync(CreateProfile createProfile)
    {
        var profile = Domain.Profile.Create(
                createProfile.Name,
                createProfile.Email, 
                createProfile.Password, 
                Role.Create(createProfile.Role.RoleId, createProfile.Role.RoleName),
                createProfile.CreatedAt
                );

        return profile;
    }

    public Domain.Profile CreateProfileAsync(ProfileDataModel profileDataModel)
    {
        var profile = Domain.Profile.Create(
                profileDataModel.Name, 
                profileDataModel.Email, 
                profileDataModel.Password, 
                Role.Create(profileDataModel.Role.RoleId, profileDataModel.Role.RoleName), 
                profileDataModel.Created_at
            );

        return profile;
    }

    public Domain.Profile UpdateProfileAsync(UpdateProfile updateProfile)
    {
        var profile = Domain.Profile.Update(
                updateProfile.Id, 
                updateProfile.Name, 
                updateProfile.Email, 
                updateProfile.Password, 
                Role.Update(updateProfile.Role.RoleId, updateProfile.Role.RoleName),
                updateProfile.CreatedAt
            );

        return profile;
    }

    public Domain.Profile UpdateProfileAsync(ProfileDataModel profileDataModel)
    {
        var profile = Domain.Profile.Update(
                profileDataModel.UserId,
                profileDataModel.Name,
                profileDataModel.Email,
                profileDataModel.Password,
                Role.Update(profileDataModel.Role.RoleId, profileDataModel.Role.RoleName),
                profileDataModel.Created_at
            );

        return profile;
    }
}
