using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviePlatform.Data;
using MoviePlatform.Exceptions;

namespace MoviePlatform.CQRS.Movie.Command;

public record DeleteMovieCommand(long Id) : IRequest<bool>;

public class DeleteMovieCommandHandler(
    ApplicationContext context,
    ILogger<DeleteMovieCommandHandler> logger) : IRequestHandler<DeleteMovieCommand, bool>
{
    public async Task<bool> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await context.Movies
                        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                    ?? throw new ResourceNotFoundException($"Movie with id {request.Id} not found");

        context.Movies.Remove(movie);
        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Movie {Id} deleted", request.Id);
        return true;
    }
}