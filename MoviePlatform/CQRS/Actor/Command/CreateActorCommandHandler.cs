using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviePlatform.Data;
using MoviePlatform.DTO.Request;
using MoviePlatform.DTO.Response;
using MoviePlatform.Exceptions;
using MoviePlatform.Mapping;

namespace MoviePlatform.CQRS.Actor.Command;

public record CreateActorCommand(ActorRequest Request) : IRequest<ActorResponse>;

public class CreateActorCommandHandler(
    ApplicationContext context,
    ILogger<CreateActorCommandHandler> logger,
    IValidator<ActorRequest> validator) : IRequestHandler<CreateActorCommand, ActorResponse>
{
    public async Task<ActorResponse> Handle(CreateActorCommand request, CancellationToken cancellationToken)
    {
        var validation = await validator.ValidateAsync(request.Request, cancellationToken);

        if (!validation.IsValid)
        {
            throw new BadRequestException(
                string.Join("; ", validation.Errors.Select(e => e.ErrorMessage)));
        }

        var isDuplicate = await context.Actors.AnyAsync(
            x => x.Name.ToLower() == request.Request.Name.ToLower()
                 && x.BirthDate == request.Request.BirthDate,
            cancellationToken);

        if (isDuplicate)
            throw new ResourceIsAlreadyExistException("Actor with this name and birth date already exists");

        var actor = ActorMapping.ToEntity(request.Request);

        context.Actors.Add(actor);
        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Actor {Id} created", actor.Id);
        return ActorMapping.ToResponse(actor);
    }
}