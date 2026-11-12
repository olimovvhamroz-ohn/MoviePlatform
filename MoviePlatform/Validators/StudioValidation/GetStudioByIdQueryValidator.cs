using FluentValidation;
using MoviePlatform.CQRS.Studio.Query;

namespace MoviePlatform.Validators.StudioValidation;

public class GetStudioByIdQueryValidator : AbstractValidator<GetStudioByIdQuery>
{
    public GetStudioByIdQueryValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}