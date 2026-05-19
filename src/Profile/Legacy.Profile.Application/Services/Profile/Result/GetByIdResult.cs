
namespace Legacy.Profile.Application.Services.Profile.Result;

public class GetByIdResult
{
    public static Domain.Profile Success(Domain.Profile profile) => profile;

    public static Domain.Profile? Empty() => null;
}
