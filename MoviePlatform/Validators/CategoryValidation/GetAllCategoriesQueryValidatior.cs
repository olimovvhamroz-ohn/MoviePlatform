using FluentValidation;
using MoviePlatform.CQRS.Actor.Query;
using MoviePlatform.CQRS.Category.Query;

namespace MoviePlatform.Validators.CategoryValidation;

public class GetAllCategoriesQueryValidatior: AbstractValidator<GetAllCategoriesQuery>
{
    public GetAllCategoriesQueryValidatior()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100);
    }
}