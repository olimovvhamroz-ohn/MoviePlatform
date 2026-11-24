using EngagementService.Data;
using EngagementService.DTO.Request;
using EngagementService.DTO.Response;
using EngagementService.Exception;
using EngagementService.Grpc;
using EngagementService.Mapping;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EngagementService.CQRS.Favorite.Command;

public record CreateFavoriteMovieCommand(FavoriteRequest FavoriteRequest,long UserId) : IRequest<FavoriteResponse>;

public class CreateFavoriteMovieCommandHandler(
    EngagementDbContext context,
    MovieGrpcClient movieGrpcClient) : IRequestHandler<CreateFavoriteMovieCommand, FavoriteResponse>
{
    public async Task<FavoriteResponse> Handle(CreateFavoriteMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await movieGrpcClient.GetMovieById(
            request.FavoriteRequest.MovieId);

        if (movie is null)
            throw new ResourceNotFoundException("Movie not found.");
        
        var isDuplicate = await context.Favorites.AnyAsync(
            x => x.UserId == request.UserId && x.MovieId == request.FavoriteRequest.MovieId,
            cancellationToken);

        if (isDuplicate)
        {
            throw new ResourceIsAlreadyExistException("You have already favorited this movie");
        }

        var favorite = FavoriteMapping.ToEntity(request.FavoriteRequest,request.UserId);
        context.Favorites.Add(favorite);
        await context.SaveChangesAsync(cancellationToken);

        return FavoriteMapping.ToResponse(favorite);
    }
}