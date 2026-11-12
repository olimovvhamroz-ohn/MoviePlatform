using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviePlatform.Data;
using MoviePlatform.DTO.Request;
using MoviePlatform.DTO.Response;
using MoviePlatform.Exceptions;
using MoviePlatform.Mapping;

namespace MoviePlatform.CQRS.Movie.Command;

public record CreateMovieCommand(MovieRequest MovieRequest) : IRequest<MovieResponse>;

public class CreateMovieCommandHandler(
    ApplicationContext context)
    : IRequestHandler<CreateMovieCommand, MovieResponse>
{
    public async Task<MovieResponse> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {

        var isDuplicate = await context.Movies.AnyAsync(x => x.Title == request.MovieRequest.Title
                                                             && x.ReleaseYear == request.MovieRequest.ReleaseYear,
            cancellationToken);

        if (isDuplicate)
        {
            throw new ResourceIsAlreadyExistException("The record already exists");
        }

        var category = await context.Categories
                           .FirstOrDefaultAsync(x => x.Id == request.MovieRequest.CategoryId, cancellationToken)
                       ?? throw new ResourceNotFoundException("Category  not found");

        var studio = await context.Studios
                         .FirstOrDefaultAsync(x => x.Id == request.MovieRequest.StudioId, cancellationToken)
                     ?? throw new ResourceNotFoundException("Studio not found");


        var actors = await context.Actors
            .Where(x => request.MovieRequest.ActorIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        if (actors.Count != request.MovieRequest.ActorIds.Distinct().Count())
        {
            throw new ResourceNotFoundException(" Actor not found");
        }

        var movie = MovieMapping.ToEntity(request.MovieRequest);
        movie.Actors = actors;
        movie.Category = category;
        movie.Studio = studio;

        context.Add(movie);
        await context.SaveChangesAsync(cancellationToken);
        return MovieMapping.ToResponse(movie);
    }
}