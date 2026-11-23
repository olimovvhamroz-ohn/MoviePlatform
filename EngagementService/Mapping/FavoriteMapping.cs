using EngagementService.DTO.Request;
using EngagementService.DTO.Response;
using EngagementService.Entities;

namespace EngagementService.Mapping;

public class FavoriteMapping
{
    public static FavoriteEntity ToEntity(
        FavoriteRequest request,
        long userId)
    {
        return new FavoriteEntity
        {
            UserId = userId,
            MovieId = request.MovieId
        };
    }

    public static FavoriteResponse ToResponse(
        FavoriteEntity entity)
    {
        return new FavoriteResponse
        {
            Id = entity.Id,
            UserId = entity.UserId,
            MovieId = entity.MovieId
        };
    }
}