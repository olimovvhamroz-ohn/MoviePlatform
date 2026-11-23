using EngagementService.CQRS.Review.Query;
using FluentValidation;

namespace EngagementService.Validators.ReviewValidation;

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