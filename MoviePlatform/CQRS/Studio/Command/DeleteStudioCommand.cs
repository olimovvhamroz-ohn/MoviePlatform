using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviePlatform.Data;
using MoviePlatform.Exceptions;

namespace MoviePlatform.CQRS.Studio.Command;

public record DeleteStudioCommand(long Id) : IRequest<bool>;

public class DeleteStudioCommandHandler(
    ApplicationContext context,
    ILogger<DeleteStudioCommandHandler> logger) : IRequestHandler<DeleteStudioCommand, bool>
{
    public async Task<bool> Handle(DeleteStudioCommand request, CancellationToken cancellationToken)
    {
        var studio = await context.Studios
                         .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                     ?? throw new ResourceNotFoundException($"Studio with id {request.Id} not found");

        var hasMovies = await context.Movies
            .AnyAsync(x => x.StudioId == request.Id, cancellationToken);
        if (hasMovies)
            throw new BadRequestException("Studio has movies and cannot be deleted");

        context.Studios.Remove(studio);
        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Studio {Id} deleted", request.Id);
        return true;
    }
}