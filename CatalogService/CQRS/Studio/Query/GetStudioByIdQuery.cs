using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviePlatform.Data;
using MoviePlatform.DTO.Response;
using MoviePlatform.Exceptions;
using MoviePlatform.Mapping;

namespace MoviePlatform.CQRS.Studio.Query;

public record GetStudioByIdQuery(long Id) : IRequest<StudioResponse>;

public class GetStudioByIdQueryHandler(
    ApplicationContext context) : IRequestHandler<GetStudioByIdQuery, StudioResponse>
{
    public async Task<StudioResponse> Handle(GetStudioByIdQuery request, CancellationToken cancellationToken)
    {
        var studio = await context.Studios
                         .AsNoTracking()
                         .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                     ?? throw new ResourceNotFoundException($"Studio with id {request.Id} not found");

        return StudioMapping.ToResponse(studio);
    }
}