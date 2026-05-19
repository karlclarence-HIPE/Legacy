using FluentValidation;

namespace Legacy.Profile.Application.Configuration.Validator;

public class ModuleConfigurationValidator: AbstractValidator<ModuleConfigurationOptions>
{
    public ModuleConfigurationValidator()
    {
        RuleFor(m => m.UploadDirectory)
            .NotEmpty()
            .WithMessage("UploadDirectory is required.");

        RuleFor(m => m.ModuleKey)
            .NotEmpty()
            .WithMessage("ModuleKey is required.");

        RuleFor(m => m.ModuleCode)
            .NotEmpty()
            .WithMessage("ModuleCode is required.");
    }
}
