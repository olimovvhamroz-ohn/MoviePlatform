using FluentValidation;
using MoviePlatform.CQRS.Actor.Command;

namespace MoviePlatform.Validators.ActorValidation;

public class DeleteActorCommandValidator : AbstractValidator<DeleteActorCommand>
{
    public DeleteActorCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}