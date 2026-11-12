using FluentValidation;
using MoviePlatform.CQRS.Studio.Query;

namespace MoviePlatform.Validators.StudioValidation;

public class GetAllStudiosQueryValidator : AbstractValidator<GetAllStudiosQuery>
{
    public GetAllStudiosQueryValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100);
    }
}