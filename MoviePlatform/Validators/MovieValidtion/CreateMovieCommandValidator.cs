using FluentValidation;
using MoviePlatform.SQRS.Movie.Command;

namespace MoviePlatform.Validators.MovieValidtion;

public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
{
    public CreateMovieCommandValidator()
    {
        RuleFor(x => x.MovieRequest).NotNull().SetValidator(new MovieRequestValidator());
    }
}