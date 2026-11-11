using FluentValidation;
using MoviePlatform.CQRS.Movie.Command;

namespace MoviePlatform.Validators.MovieValidation;

public class UpdateMovieCommandValidatior: AbstractValidator<UpdateMovieCommand>
{
    public UpdateMovieCommandValidatior()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Request.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Request.Description).NotEmpty().MaximumLength(2000);
        RuleFor(x => x.Request.Price).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Request.DurationInMinutes).GreaterThan(0);
        RuleFor(x => x.Request.ReleaseYear).InclusiveBetween(1888, DateTime.UtcNow.Year + 5);
        RuleFor(x => x.Request.AgeRating).IsInEnum();
        RuleFor(x => x.Request.CategoryId).GreaterThan(0);
        RuleFor(x => x.Request.StudioId).GreaterThan(0);
        RuleForEach(x => x.Request.ActorIds).GreaterThan(0);
    }
}
