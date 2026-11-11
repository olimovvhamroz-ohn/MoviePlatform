using FluentValidation;
using MoviePlatform.CQRS.Movie.Query;

namespace MoviePlatform.Validators.MovieValidation;

public class GetMovieByIdQueryValidatior : AbstractValidator<GetMovieByIdQuery>
{
    public GetMovieByIdQueryValidatior()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}