using Legacy.Authentication.Application.Common;
using Legacy.Profile.Application.Common;
using Legacy.Profile.Application.Common.Data;
using Legacy.Profile.Application.Common.Mapping;

namespace Legacy.Profile.Application.Services.Profile.Repository;

public interface IProfileRepository
{
    Task<bool> CreateAsync(Domain.Profile entity, CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(Domain.Profile entity, CancellationToken cancellationToken);

    Task<IDictionary<int, ProfileDataModel>> GetAllAsync(GetAllOptions options, CancellationToken cancellationToken = default);
    
    Task<int> GetRecordCountAsync(GetAllOptions options, CancellationToken cancellationToken = default);

    Task<ProfileDataModel> GetByIdAsync(int userId, CancellationToken cancellationToken = default);

    Task<bool> ValidateIfExistAsync(string parameter, CancellationToken cancellationToken = default);
}
