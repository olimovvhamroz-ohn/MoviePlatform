using FluentValidation;
using MoviePlatform.CQRS.Review.Query;

namespace MoviePlatform.Validators.ReviewValidation;

public class GetReviewsByMovieQueryValidator : AbstractValidator<GetReviewsByMovieQuery>
{
    public GetReviewsByMovieQueryValidator()
    {
        RuleFor(x => x.MovieId)
            .GreaterThan(0);

        RuleFor(x => x.Page)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100);
    }
}