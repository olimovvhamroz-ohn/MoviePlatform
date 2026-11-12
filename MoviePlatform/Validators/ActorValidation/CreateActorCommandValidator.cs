using FluentValidation;
using MoviePlatform.CQRS.Actor.Command;

namespace MoviePlatform.Validators.ActorValidation;

public class CreateActorCommandValidator : AbstractValidator<CreateActorCommand>
{
    public CreateActorCommandValidator()
    {
        RuleFor(x => x.Request.Name).NotEmpty().MaximumLength(150);
        RuleFor(x => x.Request.BirthDate)
            .GreaterThan(new DateOnly(1850, 1, 1))
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow));
        RuleFor(x => x.Request.Biography).MaximumLength(4000);
    }
}