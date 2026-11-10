using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviePlatform.Data;
using MoviePlatform.DTO.Request;
using MoviePlatform.DTO.Response;
using MoviePlatform.Exceptions;
using MoviePlatform.Mapping;

namespace MoviePlatform.CQRS.Movie.Command;

public record UpdateMovieCommand(long Id, MovieRequest Request) : IRequest<MovieResponse>;

public class UpdateMovieCommandHandler(ApplicationContext context, IMapper mapper, IValidator<MovieRequest> validator)
    : IRequestHandler<UpdateMovieCommand, MovieResponse>
{
    public async Task<MovieResponse> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Request;

        var validation = await validator.ValidateAsync(dto, cancellationToken);
        
        if (!validation.IsValid)
            throw new BadRequestException(
                string.Join("; ", validation.Errors.Select(e => e.ErrorMessage)));

        var movie = await context.Movies
                        .Include(x => x.Actors)
                        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                    ?? throw new ResourceNotFoundException($"Movie with id {request.Id} not found");

        var isDuplicate = await context.Movies.AnyAsync(
            x => x.Id != request.Id && x.Title == dto.Title && x.ReleaseYear == dto.ReleaseYear,
            cancellationToken);
        
        if (isDuplicate)
        {
            throw new ResourceIsAlreadyExistException("Movie already exists");
        }

        var category = await context.Categories
                           .FirstOrDefaultAsync(x => x.Id == dto.CategoryId, cancellationToken)
                       ?? throw new ResourceNotFoundException("Category not found");

        var studio = await context.Studios
                         .FirstOrDefaultAsync(x => x.Id == dto.StudioId, cancellationToken)
                     ?? throw new ResourceNotFoundException("Studio not found");

        var actors = await context.Actors
            .Where(x => dto.ActorIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        if (actors.Count != dto.ActorIds.Distinct().Count())
        {
            throw new ResourceNotFoundException("Some actors not found");
        }

        MovieMapping.UpdateEntity(movie, dto);
        movie.Category = category;
        movie.Studio = studio;
        movie.Actors.Clear();
        foreach (var actor in actors)
            movie.Actors.Add(actor);

        await context.SaveChangesAsync(cancellationToken);

        return MovieMapping.ToResponse(movie);
    }
}