using AutoMapper;
using MoviePlatform.DTO.Request;
using MoviePlatform.DTO.Response;
using MoviePlatform.Entities;

namespace MoviePlatform.Mapping;

public class MovieMappingProfile:Profile
{
    public MovieMappingProfile()
    {
        CreateMap<MovieEntity, MovieResponse>();
       CreateMap<MovieRequest,MovieResponse>();
    }
}