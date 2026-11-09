using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviePlatform.Data;
using MoviePlatform.DTO.Response;

namespace MoviePlatform.CQRS.Actor.Query;

public record GetAllActorsQuery(int Page, int PageSize) : IRequest<List<ActorResponse>>;

public class GetAllActorsQueryHandler(
    ApplicationContext context,
    IMapper mapper) : IRequestHandler<GetAllActorsQuery, List<ActorResponse>>
{
    public async Task<List<ActorResponse>> Handle(
        GetAllActorsQuery request, CancellationToken cancellationToken)
    {
        var page = Math.Max(request.Page, 1);
        var pageSize = Math.Clamp(request.PageSize, 1, 100);

        var actor = await context.Actors
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ThenBy(x => x.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return mapper.Map<List<ActorResponse>>(actor);
    }
}