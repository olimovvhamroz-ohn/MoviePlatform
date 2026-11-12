
using MoviePlatform.DTO.Request;
using MoviePlatform.DTO.Response;
using MoviePlatform.Entities;

namespace MoviePlatform.Mapping;

public static class MovieMapping
{
    public static MovieEntity ToEntity(MovieRequest request)
    {
        return new MovieEntity
        {
            Title = request.Title,
            Price = request.Price,
            Description = request.Description,
            DurationInMinutes = request.DurationInMinutes,
            ReleaseYear = request.ReleaseYear,
            PosterUrl = request.PosterUrl,
            AgeRating = request.AgeRating,
            CategoryId = request.CategoryId,
            StudioId = request.StudioId
        };
    }

    public static void UpdateEntity(
        MovieEntity entity,
        MovieRequest request)
    {
        entity.Title = request.Title;
        entity.Price = request.Price;
        entity.Description = request.Description;
        entity.DurationInMinutes = request.DurationInMinutes;
        entity.ReleaseYear = request.ReleaseYear;
        entity.PosterUrl = request.PosterUrl;
        entity.AgeRating = request.AgeRating;
        entity.CategoryId = request.CategoryId;
        entity.StudioId = request.StudioId;
    }

    public static MovieResponse ToResponse(MovieEntity entity)
    {
        var response = new MovieResponse
        {
            Title = entity.Title,
            Price = entity.Price,
            Description = entity.Description,
            DurationInMinutes = entity.DurationInMinutes,
            ReleaseYear = entity.ReleaseYear,
            PosterUrl = entity.PosterUrl,
            AgeRating = entity.AgeRating,
            AverageRating = (double)entity.AverageRating,
            ReviewsCount = entity.ReviewsCount,
            CategoryId = entity.CategoryId,
            StudioId = entity.StudioId,

            Category = CategoryMapping.ToResponse(entity.Category),

            Studio = StudioMapping.ToResponse(entity.Studio)
        };

        foreach (var actor in entity.Actors)
        {
            response.Actors.Add(
                ActorMapping.ToResponse(actor));
        }

        return response;
    }
}
