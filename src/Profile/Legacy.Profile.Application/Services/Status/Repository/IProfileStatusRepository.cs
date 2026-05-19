using Legacy.Profile.Application.Common.Data;
namespace Legacy.Profile.Application.Services.Status.Repository;

public interface IProfileStatusRepository
{
    Task<ProfileStatusDataModel?> GetByName(string status, CancellationToken cancellationToken);

}
