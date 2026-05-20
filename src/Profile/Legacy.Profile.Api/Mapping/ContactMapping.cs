using Legacy.Profile.Application.Common;
using Legacy.Profile.Application.Common.Mapping;
using Legacy.Profile.Application.Domain;
using Legacy.Profile.Contracts.Request;
using Legacy.Profile.Contracts.Response;
using Legacy.Shared.Options;
using GetAllOptions = Legacy.Profile.Application.Common.Mapping.GetAllOptions;

namespace Legacy.Profile.Api.Mapping;

public static class ContactMapping
{
    public static CreateProfile Map(this ProfileRequest request) =>
        new()
        { 
            Name = request.Name, 
            Email = request.Email,
            Password = request.Password, 
            Role = request.Role.Map(), 
            CreatedAt = request.CreatedAt, 
            UpdatedAt = request.UpdatedAt
        };

    public static RoleModel Map(this RoleRequest request) =>
        new()
        {
            RoleId = request.RoleId,
            RoleName = request.RoleName,
        };

    public static ProfileResponse Map(this Application.Domain.Profile profile) =>
        new()
        {
            UserId = profile.UserId,
            Name = profile.Name,
            Email = profile.Email,
            Password = profile.Password,
            Role = profile.Role.Map(),
            CreateAt = profile.CreatedAt,
            UpdatedAt = profile.UpdatedAt
        };

    public static RoleResponse Map(this Role role) =>
        new()
        {
            RoleId = role.RoleId,
            RoleName = role.RoleName
        };

    public static UpdateProfile Map(this UpdateProfileRequest request) =>
        new()
        { 
            UserId = request.UserId, 
            Name = request.Name, 
            Email = request.Email, 
            Password = request.Password, 
            Role = request.Role.Map(), 
            CreatedAt = request.CreatedAt, 
            UpdatedAt = request.UpdatedAt
        };
}
