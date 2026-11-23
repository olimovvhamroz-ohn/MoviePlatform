using EngagementService.CQRS.Favorite.Command;
using FluentValidation;

namespace EngagementService.Validators.FavoriteValidation;

public class CreateFavoriteMovieCommandValidator
    : AbstractValidator<CreateFavoriteMovieCommand>
{
    public CreateFavoriteMovieCommandValidator()
    {
        RuleFor(x => x.FavoriteRequest.MovieId)
            .GreaterThan(0)
            .WithMessage("MovieId must be greater than 0");

        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("UserId must be greater than 0");
    }
}