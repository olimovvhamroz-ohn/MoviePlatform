using FluentValidation;
using MoviePlatform.CQRS.Actor.Query;

namespace MoviePlatform.Validators.ActorValidation;

public class GetAllActorsQueryValidator : AbstractValidator<GetAllActorsQuery>
{
    public GetAllActorsQueryValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100);
    }
}