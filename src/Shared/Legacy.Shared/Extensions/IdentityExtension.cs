using Legacy.Shared.Authentication;
using Legacy.Shared.Identity.Request;
using Legacy.Shared.Options;
using Microsoft.AspNetCore.Http;
using static System.Convert;

namespace Legacy.Shared.Extensions;

public static class IdentityExtension
{
    public static UserOptions? GetUserOptions(this HttpContext context)
    {
        var userId = context.User.Claims.SingleOrDefault(c => c.Type == ApplicationClaimTypes.UserId);
        
        if (userId == null) return null;

        return new UserOptions
        {
            UserId = ToInt32(userId.Value)
        };
    }

    public static UserIdentityRequest? GetUserInfo(this HttpContext context)
    {
        var userId = context.User.Claims.SingleOrDefault(c => c.Type == ApplicationClaimTypes.UserId);
        var userName = context.User.Claims.SingleOrDefault(c => c.Type == ApplicationClaimTypes.UserName);
        var email = context.User.Claims.SingleOrDefault(c => c.Type == ApplicationClaimTypes.Email);
        var password = context.User.Claims.SingleOrDefault(c => c.Type == ApplicationClaimTypes.Password);

        if (userId == null || userName == null || email == null || password == null) return null;
        
        return UserIdentityRequest.Create(userName.Value, email.Value, password.Value);
    }
}
