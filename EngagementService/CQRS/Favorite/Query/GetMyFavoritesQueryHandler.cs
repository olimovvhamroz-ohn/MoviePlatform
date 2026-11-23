using EngagementService.Data;
using EngagementService.DTO.Response;
using EngagementService.Mapping;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EngagementService.CQRS.Favorite.Query;

public record GetMyFavoritesQuery(
    long UserId,
    int Page,
    int PageSize
) : IRequest<List<FavoriteResponse>>;

public class GetMyFavoritesQueryHandler(EngagementDbContext context)
    : IRequestHandler<GetMyFavoritesQuery, List<FavoriteResponse>>
{
    public async Task<List<FavoriteResponse>> Handle(
        GetMyFavoritesQuery request,
        CancellationToken cancellationToken)
    {
        var page = Math.Max(request.Page, 1);
        var pageSize = Math.Clamp(request.PageSize, 1, 100);

        var favorites = await context.Favorites
            .AsNoTracking()
            .Where(x => x.UserId == request.UserId)
            .OrderByDescending(x => x.CreatedAt)
            .ThenBy(x => x.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return favorites
            .Select(FavoriteMapping.ToResponse)
            .ToList();
    }
}