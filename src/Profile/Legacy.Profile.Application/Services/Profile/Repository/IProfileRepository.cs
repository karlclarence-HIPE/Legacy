using Legacy.Authentication.Application.Common;
using Legacy.Profile.Application.Common;
using Legacy.Profile.Application.Common.Data;

namespace Legacy.Profile.Application.Services.Profile.Repository;

public interface IProfileRepository
{
    Task<bool> CreateAsync(Domain.Profile entity, CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(Domain.Profile entity, CancellationToken cancellationToken);
    //Task<IDictionary<int, ProfileDataModel>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<ProfileDataModel> GetByIdAsync(int userId, CancellationToken cancellationToken = default);
}
