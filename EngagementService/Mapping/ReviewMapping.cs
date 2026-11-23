using EngagementService.DTO.Request;
using EngagementService.DTO.Response;
using EngagementService.Entities;

namespace EngagementService.Mapping;

public static class ReviewMapping
{
    public static ReviewEntity ToEntity(ReviewRequest request)
    {
        return new ReviewEntity
        {
            UserId = request.UserId,
            MovieId = request.MovieId,
            Rating = request.Rating,
            Comment = request.Comment
        };
    }


    public static ReviewResponse ToResponse(ReviewEntity entity)
    {
        return new ReviewResponse
        {
            Id = entity.Id,
            UserId = entity.UserId,
            MovieId = entity.MovieId,
            Rating = entity.Rating,
            Comment = entity.Comment,
            CreatedAt = entity.CreatedAt
        };
    }
    
    
}