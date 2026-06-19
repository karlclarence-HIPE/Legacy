namespace Legacy.Profile.Application.Common;

public class UpdateProfile : BaseProfile
{
    public required int UserId { get; set; }

    public required DateTime UpdatedAt { get; init; }
}
