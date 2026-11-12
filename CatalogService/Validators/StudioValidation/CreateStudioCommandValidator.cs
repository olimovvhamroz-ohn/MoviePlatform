using FluentValidation;
using MoviePlatform.CQRS.Studio.Command;

namespace MoviePlatform.Validators.StudioValidation;

public class CreateStudioCommandValidator : AbstractValidator<CreateStudioCommand>
{
    public CreateStudioCommandValidator()
    {
        RuleFor(x => x.Studio.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Studio.FoundYear).InclusiveBetween(1850, DateTime.UtcNow.Year);
    }
}