using Legacy.Authentication.Application.Services.Token.Model;

namespace Legacy.Authentication.Application.Services.Token.Repository;

public interface ITokenRepository
{
    Task<bool> SaveTokenAsync(AuthenticationToken token, CancellationToken cancellationToken = default);


}
