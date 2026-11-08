using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviePlatform.Data;
using MoviePlatform.Exceptions;

namespace MoviePlatform.CQRS.Actor.Command;

public record DeleteActorCommand(long Id) : IRequest<bool>;

public class DeleteActorCommandHandler(
    ApplicationContext context,
    ILogger<DeleteActorCommandHandler> logger) : IRequestHandler<DeleteActorCommand, bool>
{
    public async Task<bool> Handle(DeleteActorCommand request, CancellationToken cancellationToken)
    {
        var actor = await context.Actors
                         .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                     ?? throw new ResourceNotFoundException($"Actor with id {request.Id} not found");

        context.Actors.Remove(actor);
        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Actor {Id} deleted", request.Id);
        return true;
    }
}