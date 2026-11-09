using FluentValidation;
using MoviePlatform.DTO.Request;

namespace MoviePlatform.Validators.ActorValidation;

public class ActorRequestValidator : AbstractValidator<ActorRequest>
{
    public ActorRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(150);
        RuleFor(x => x.BirthDate)
            .GreaterThan(new DateOnly(1850, 1, 1))
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow));
        RuleFor(x => x.Biography).MaximumLength(4000);
    }
}