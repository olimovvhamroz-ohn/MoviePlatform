using EngagementService.Data;
using EngagementService.DTO.Request;
using EngagementService.Exception;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EngagementService.CQRS.Favorite.Command;

public record DeleteFavoriteMovieCommand(FavoriteRequest FavoriteRequest, long UserId) : IRequest<bool>;

public class DeleteFavoriteMovieCommandHandler(
    EngagementDbContext context) : IRequestHandler<DeleteFavoriteMovieCommand, bool>
{
    public async Task<bool> Handle(DeleteFavoriteMovieCommand request, CancellationToken cancellationToken)
    {
        var favorite = await context.Favorites.FirstOrDefaultAsync(
            x => x.UserId == request.UserId
                 && x.MovieId == request.FavoriteRequest.MovieId,
            cancellationToken);

        if (favorite == null)
        {
            throw new ResourceNotFoundException("Favorite not found");
        }

        context.Favorites.Remove(favorite);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}