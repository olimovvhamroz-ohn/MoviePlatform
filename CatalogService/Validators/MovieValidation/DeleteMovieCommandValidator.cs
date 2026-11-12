using FluentValidation;
using MoviePlatform.CQRS.Movie.Command;

namespace MoviePlatform.Validators.MovieValidation;

public class DeleteMovieCommandValidator : AbstractValidator<DeleteMovieCommand>
{
    public DeleteMovieCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().GreaterThan(0);
    }
}