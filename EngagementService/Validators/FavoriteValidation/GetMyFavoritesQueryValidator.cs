using EngagementService.CQRS.Favorite.Query;
using FluentValidation;

namespace EngagementService.Validators.FavoriteValidation;

public class GetMyFavoritesQueryValidator
    : AbstractValidator<GetMyFavoritesQuery>
{
    public GetMyFavoritesQueryValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("UserId must be greater than 0");

        RuleFor(x => x.Page)
            .GreaterThan(0)
            .WithMessage("Page must be greater than 0");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100)
            .WithMessage("PageSize must be between 1 and 100");
    }
}