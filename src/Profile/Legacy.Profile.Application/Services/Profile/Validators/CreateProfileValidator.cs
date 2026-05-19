using FluentValidation;
using Legacy.Profile.Application.Common;
using Legacy.Profile.Application.Services.Profile.Errors;

namespace Legacy.Profile.Application.Services.Profile.Validators;

public class CreateProfileValidator : AbstractValidator<CreateProfile>
{
    public CreateProfileValidator()
    {

    }
}
