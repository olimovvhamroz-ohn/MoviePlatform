using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviePlatform.Data;
using MoviePlatform.DTO.Response;

namespace MoviePlatform.CQRS.Studio.Query;

public record GetAllStudiosQuery(int Page, int PageSize) : IRequest<List<StudioResponse>>;

public class GetAllStudiosQueryHandler(
    ApplicationContext context,
    IMapper mapper) : IRequestHandler<GetAllStudiosQuery, List<StudioResponse>>
{
    public async Task<List<StudioResponse>> Handle(
        GetAllStudiosQuery request, CancellationToken cancellationToken)
    {
        var page = Math.Max(request.Page, 1);
        var pageSize = Math.Clamp(request.PageSize, 1, 100);

        var studios = await context.Studios
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ThenBy(x => x.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return mapper.Map<List<StudioResponse>>(studios);
    }
}