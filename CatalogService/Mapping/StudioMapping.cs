using MoviePlatform.DTO.Request;
using MoviePlatform.DTO.Response;
using MoviePlatform.Entities;

namespace MoviePlatform.Mapping;

public static class StudioMapping
{
    public static StudioEntity ToEntity(StudioRequest request)
    {
        return new StudioEntity
        {
            Name = request.Name,
            FoundYear = request.FoundYear
        };
    }
    
    public static StudioResponse ToResponse(StudioEntity entity)
    {
        return new StudioResponse
        {
            Name = entity.Name,
            FoundYear = entity.FoundYear
        };
    }
}