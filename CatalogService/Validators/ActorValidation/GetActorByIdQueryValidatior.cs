using FluentValidation;
using MoviePlatform.CQRS.Actor.Query;

namespace MoviePlatform.Validators.ActorValidation;

public class GetActorByIdQueryValidatior:AbstractValidator<GetActorByIdQuery>
{
    public GetActorByIdQueryValidatior()
    {
        RuleFor(x => x.Id).GreaterThan(0);

    }
}