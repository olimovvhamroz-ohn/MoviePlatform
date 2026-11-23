using EngagementService.CQRS.Favorite.Query;
using FluentValidation;

namespace EngagementService.Validators.FavoriteValidation;

public class CheckFavoriteQueryValidator
    : AbstractValidator<CheckFavoriteQuery>
{
    public CheckFavoriteQueryValidator()
    {
        RuleFor(x => x.MovieId)
            .GreaterThan(0)
            .WithMessage("MovieId must be greater than 0");

        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("UserId must be greater than 0");
    }
}