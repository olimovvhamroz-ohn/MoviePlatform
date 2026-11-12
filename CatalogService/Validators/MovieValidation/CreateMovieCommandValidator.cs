using FluentValidation;
using MoviePlatform.CQRS.Movie.Command;

namespace MoviePlatform.Validators.MovieValidation;

public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
{
    public CreateMovieCommandValidator()
    {
        RuleFor(x => x.MovieRequest.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.MovieRequest.Description).NotEmpty().MaximumLength(2000);
        RuleFor(x => x.MovieRequest.Price).GreaterThanOrEqualTo(0);
        RuleFor(x => x.MovieRequest.DurationInMinutes).GreaterThan(0);
        RuleFor(x => x.MovieRequest.ReleaseYear).InclusiveBetween(1888, DateTime.UtcNow.Year + 5);
        RuleFor(x => x.MovieRequest.AgeRating).IsInEnum();
        RuleFor(x => x.MovieRequest.CategoryId).GreaterThan(0);
        RuleFor(x => x.MovieRequest.StudioId).GreaterThan(0);
        RuleForEach(x => x.MovieRequest.ActorIds).GreaterThan(0);
    }
}