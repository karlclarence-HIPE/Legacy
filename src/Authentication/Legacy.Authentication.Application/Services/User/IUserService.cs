using Legacy.Authentication.Application.Services.User.Model;
using Legacy.Authentication.Application.Services.Common;
using Legacy.Shared.Utility;

namespace Legacy.Authentication.Application.Services.User;

public interface IUserService
{
    Task<Result<AuthenticationResult, AuthenticationFailureResult>> LoginByUsernameAsync(Login request, CancellationToken cancellationToken = default);

    //Task<Result<>> RegisterUserAsync(CancellationToken cancellationToken = default);


}
