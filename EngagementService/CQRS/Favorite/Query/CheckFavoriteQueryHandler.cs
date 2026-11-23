using EngagementService.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EngagementService.CQRS.Favorite.Query;

public record CheckFavoriteQuery(long UserId, long MovieId)
    : IRequest<bool>;

public class CheckFavoriteQueryHandler(EngagementDbContext context)
    : IRequestHandler<CheckFavoriteQuery, bool>
{
    public async Task<bool> Handle(CheckFavoriteQuery request, CancellationToken cancellationToken)
    {
        return await context.Favorites.AnyAsync(
            x => x.UserId == request.UserId &&
                 x.MovieId == request.MovieId,
            cancellationToken);
    }
}