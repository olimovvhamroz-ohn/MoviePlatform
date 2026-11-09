using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviePlatform.Data;
using MoviePlatform.DTO.Response;
using MoviePlatform.Exceptions;

namespace MoviePlatform.CQRS.Actor.Query;

public record GetActorByIdQuery(long Id) : IRequest<ActorResponse>;

public class GetActorByIdQueryHandler (
    ApplicationContext context,
    IMapper mapper) : IRequestHandler<GetActorByIdQuery, ActorResponse>
{
    public async Task<ActorResponse> Handle(GetActorByIdQuery request, CancellationToken cancellationToken)
    {
        var actor = await context.Actors
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                    ?? throw new ResourceNotFoundException($"Actor with id {request.Id} not found");

        return mapper.Map<ActorResponse>(actor);
    }
}