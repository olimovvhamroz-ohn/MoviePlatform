using MoviePlatform.DTO.Request;
using MoviePlatform.DTO.Response;
using MoviePlatform.Entities;

namespace MoviePlatform.Mapping;

public static class ActorMapping
{
    public static ActorEntity ToEntity(ActorRequest request)
    {
        return new ActorEntity
        {
            Name = request.Name,
            BirthDate = request.BirthDate,
            Biography = request.Biography
        };
    }
    
    public static ActorResponse ToResponse(ActorEntity entity)
    {
        return new ActorResponse
        {
            Name = entity.Name,
            BirthDate = entity.BirthDate,
            Biography = entity.Biography
        };
    }
}