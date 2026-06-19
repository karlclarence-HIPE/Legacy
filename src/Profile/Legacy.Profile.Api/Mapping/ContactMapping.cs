using Legacy.Profile.Application.Common;
using Legacy.Profile.Application.Common.Mapping;
using Legacy.Profile.Application.Domain;
using Legacy.Profile.Application.Services.Profile.Result;
using Legacy.Profile.Contracts.Request;
using Legacy.Profile.Contracts.Response;
using Legacy.Shared.Options;
using DateRangeDto = Legacy.Profile.Application.Common.Mapping.DateRangeDto;
using GetAllOptions = Legacy.Profile.Application.Common.Mapping.GetAllOptions;

namespace Legacy.Profile.Api.Mapping;

public static class ContactMapping
{
    public static GetAllOptions? Map(this GetAllRequest? request)
    {
        if (request?.DateField is null || string.IsNullOrEmpty(request.DateField) )
        {

            return new GetAllOptions
            {
                Name = request?.Name,
                RoleIds = request?.RoleIds ?? [0],
                SortField = request?.SortBy?.Trim('+', '-'),
                SortOrder = request.SortBy is null
                    ? SortOrder.Unsorted 
                    : request.SortBy.StartsWith('-')
                        ? SortOrder.Descending
                        : SortOrder.Ascending,
                Page = request.Page,
                PageSize = request.PageSize
            };
        }

        if (string.IsNullOrEmpty(request.DateFrom) || string.IsNullOrEmpty(request.DateTo))
            return null;

        return new GetAllOptions
        {
            Name = request.Name,
            RoleIds = request.RoleIds ?? [0],
            DateRange = new DateRangeDto
            {
                CreatedAt = request.CreatedAt, 
                UpdatedAt = request.UpdateAt,
            },
            Page = request.Page, 
            PageSize = request.PageSize,
        };

    }
    public static CreateProfile Map(this ProfileRequest request) =>
        new()
        { 
            Name = request.Name, 
            Email = request.Email,
            Password = request.Password, 
            Role = request.Role.Map(), 
            CreatedAt = request.CreatedAt
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

    public static GetAllResponse Map(this GetAllResult result, int page, int pageSize, int recordCount)
    {
        var records = result.Items.Select(i => i.Map());
        var pageCount = (recordCount % pageSize) > 0 ? (recordCount / pageSize) + 1 : (recordCount / pageSize);

        return new GetAllResponse
        {
            Items = records,
            Page = pageSize,
            PageSize = pageSize,
            PageCount = pageCount, 
            Total = recordCount
        };
    }
}
