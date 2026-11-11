using FluentValidation;
using MoviePlatform.CQRS.Movie.Query;

namespace MoviePlatform.Validators.MovieValidation;

public class GetAllMovieQueryValidator : AbstractValidator<GetAllMovieQuery>
{
    public GetAllMovieQueryValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100);

        RuleFor(x => x.CategoryId)
            .GreaterThan(0)
            .When(x => x.CategoryId.HasValue);

        RuleFor(x => x.StudioId)
            .GreaterThan(0)
            .When(x => x.StudioId.HasValue);
    }
}