using FluentValidation;
using MoviePlatform.DTO.Request;

namespace MoviePlatform.Validators.StudioValidation;

public class StudioRequestValidator : AbstractValidator<StudioRequest>
{
    public StudioRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.FoundYear).InclusiveBetween(1850, DateTime.UtcNow.Year);
    }
}