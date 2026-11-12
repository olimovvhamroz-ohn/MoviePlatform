using FluentValidation;
using MoviePlatform.CQRS.Category.Query;

namespace MoviePlatform.Validators.CategoryValidation;

public class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>
{
    public GetCategoryByIdQueryValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}