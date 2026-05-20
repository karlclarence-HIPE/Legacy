namespace Legacy.Profile.Contracts.Request;

public class UpdateProfileRequest : BaseProfileRequest
{
    public required int UserId { get; set; }
}
