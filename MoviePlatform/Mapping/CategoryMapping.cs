using MoviePlatform.DTO.Request;
using MoviePlatform.DTO.Response;
using MoviePlatform.Entities;

namespace MoviePlatform.Mapping;

public static class CategoryMapping
{
    public static CategoryEntity ToEntity(CategoryRequest request)
    {
        return new CategoryEntity
        {
            Title = request.Title
        };
    }

    public static void UpdateEntity(
        CategoryEntity entity,
        CategoryRequest request)
    {
        entity.Title = request.Title;
    }

    public static CategoryResponse ToResponse(CategoryEntity entity)
    {
        return new CategoryResponse
        {
            Id = entity.Id,
            Title = entity.Title
        };
    }
}