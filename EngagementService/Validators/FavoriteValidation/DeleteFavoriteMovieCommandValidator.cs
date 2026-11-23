using EngagementService.CQRS.Favorite.Command;
using FluentValidation;

namespace EngagementService.Validators.FavoriteValidation;

public class DeleteFavoriteMovieCommandValidator
    : AbstractValidator<DeleteFavoriteMovieCommand>
{
    public DeleteFavoriteMovieCommandValidator()
    {
        RuleFor(x => x.FavoriteRequest.MovieId)
            .GreaterThan(0)
            .WithMessage("MovieId must be greater than 0");

        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("UserId must be greater than 0");
    }
}