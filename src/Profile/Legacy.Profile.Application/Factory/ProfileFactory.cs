using Legacy.Framework.FileManager;
using Legacy.Profile.Application.Common;
using Legacy.Profile.Application.Common.Data;
using Legacy.Profile.Application.Configuration;
using Legacy.Profile.Application.Domain;
using Legacy.Shared.Provider;
using Legacy.Shared.Utility;
using Microsoft.Extensions.Options;
using Legacy.Profile.Application.Services.Profile.Models;
using System.Text.Json;

namespace Legacy.Profile.Application.Factory;

public class ProfileFactory : IProfileFactory
{
    private readonly IDateTimeProvider _dateTimeProvider; 
    private readonly IFileManager _fileManager;
    private readonly IOptionsMonitor<ModuleConfigurationOptions> _configurations;
    
    public ProfileFactory(IDateTimeProvider dateTimeProvider, IFileManager fileManager, 
        IOptionsMonitor<ModuleConfigurationOptions> configurations)
    {
        this._dateTimeProvider = dateTimeProvider;
        this._fileManager = fileManager;
        this._configurations = configurations;
    }

    public async Task<Domain.Profile> CreateProfileAsync(CreateProfile createProfile)
    {
        var profile = Domain.Profile.Create(
                createProfile.Name,
                createProfile.Email, 
                createProfile.Password, 
                Role.Create(createProfile.Role.RoleId, createProfile.Role.RoleName),
                createProfile.CreatedAt
                );

        if (createProfile.ImageUrl is null) return profile;

        var fileName = await _fileManager.UploadAsync(
            createProfile.ImageUrl,
            _configurations.CurrentValue.UploadDirectory
        );

        profile.UploadImage(fileName);

        return profile;
    }

    public Domain.Profile CreateProfileAsync(ProfileDataModel profileDataModel)
    {
        string? extractedPath = null;

       if (!string.IsNullOrEmpty(profileDataModel.ImageUrl))
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var files = JsonSerializer.Deserialize<List<DatabaseImageMetadata>>(profileDataModel.ImageUrl, options);
                extractedPath = files?.FirstOrDefault()?.Path;
            } catch (JsonException ex)
            {
                System.Diagnostics.Debug.WriteLine($"JSON Error: {ex.Message}");
            }
        }
        
        var profile = Domain.Profile.Load(
                profileDataModel.UserId,
                profileDataModel.Name, 
                profileDataModel.Email, 
                profileDataModel.Password, 
                Role.Create(1, "Admin"), 
                profileDataModel.Created_at,
                profileDataModel.Updated_at,
                extractedPath
            );

        return profile;
    }

    public Domain.Profile UpdateProfileAsync(UpdateProfile updateProfile)
    {
        var profile = Domain.Profile.Update(
                updateProfile.UserId, 
                updateProfile.Name, 
                updateProfile.Email, 
                updateProfile.Password, 
                Role.Update(updateProfile.Role.RoleId, updateProfile.Role.RoleName),
                updateProfile.CreatedAt, 
                updateProfile.UpdatedAt
            );

        if (updateProfile.ImageUrl is null) return profile;
        
        var fileName = _fileManager.UploadAsync(
            updateProfile.ImageUrl,
            _configurations.CurrentValue.UploadDirectory
        );

        profile.UploadImage(fileName.Result);

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
                profileDataModel.Created_at, 
                profileDataModel.Updated_at
            );

        return profile;
    }
}
