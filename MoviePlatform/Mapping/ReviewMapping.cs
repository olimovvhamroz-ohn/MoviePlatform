
using MoviePlatform.DTO.Request;
using MoviePlatform.DTO.Response;
using MoviePlatform.Entities;

namespace MoviePlatform.Mapping;

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

    public static void UpdateEntity(
        ReviewEntity entity,
        ReviewRequest request)
    {
        entity.UserId = request.UserId;
        entity.MovieId = request.MovieId;
        entity.Rating = request.Rating;
        entity.Comment = request.Comment;
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
