using FluentValidation;
using MoviePlatform.DTO.Request;

namespace MoviePlatform.Validators.MovieValidation;

public class MovieRequestValidator : AbstractValidator<MovieRequest>
{
    public MovieRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
        RuleFor(x => x.DurationInMinutes).GreaterThan(0);
        RuleFor(x => x.ReleaseYear).InclusiveBetween(1888, DateTime.UtcNow.Year + 5);
        RuleFor(x => x.AgeRating).IsInEnum();
        RuleFor(x => x.CategoryId).GreaterThan(0);
        RuleFor(x => x.StudioId).GreaterThan(0);
        RuleForEach(x => x.ActorIds).GreaterThan(0);
    }
}