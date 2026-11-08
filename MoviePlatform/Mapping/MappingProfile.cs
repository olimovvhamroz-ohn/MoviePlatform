using AutoMapper;
using MoviePlatform.DTO.Request;
using MoviePlatform.DTO.Response;
using MoviePlatform.Entities;

namespace MoviePlatform.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<MovieRequest, MovieEntity>()
            .ForMember(d => d.Actors, o => o.Ignore());
        CreateMap<MovieEntity, MovieResponse>();

        CreateMap<CategoryRequest, CategoryEntity>();
        CreateMap<CategoryEntity, CategoryResponse>();

        CreateMap<StudioRequest, StudioEntity>();
        CreateMap<StudioEntity, StudioResponse>();

        CreateMap<ActorRequest, ActorEntity>();
        CreateMap<ActorEntity, ActorResponse>();
        
        CreateMap<ReviewRequest, ReviewEntity>();
        CreateMap<ReviewEntity, ReviewResponse>();
    }
}