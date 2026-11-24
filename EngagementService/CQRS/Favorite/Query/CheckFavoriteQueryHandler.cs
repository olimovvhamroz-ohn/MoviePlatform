using EngagementService.Data;
using EngagementService.Exception;
using EngagementService.Grpc;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EngagementService.CQRS.Favorite.Query;

public record CheckFavoriteQuery(long UserId, long MovieId)
    : IRequest<bool>;

public class CheckFavoriteQueryHandler(EngagementDbContext context, MovieGrpcClient movieGrpcClient)
    : IRequestHandler<CheckFavoriteQuery, bool>
{
    public async Task<bool> Handle(CheckFavoriteQuery request, CancellationToken cancellationToken)
    {
        var movie = await movieGrpcClient.GetMovieById(
            request.MovieId);

        if (movie is null)
            throw new ResourceNotFoundException("Movie not found.");
        
        return await context.Favorites.AnyAsync(
            x => x.UserId == request.UserId &&
                 x.MovieId == request.MovieId,
            cancellationToken);
    }
}