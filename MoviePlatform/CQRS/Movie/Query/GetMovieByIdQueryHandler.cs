using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviePlatform.Data;
using MoviePlatform.DTO.Response;
using MoviePlatform.Exceptions;

namespace MoviePlatform.CQRS.Movie.Query;

public record GetMovieByIdQuery(long Id) : IRequest<MovieResponse>;

public class GetMovieByIdQueryHandler(ApplicationContext context, IMapper mapper)
    : IRequestHandler<GetMovieByIdQuery, MovieResponse>
{
    public async Task<MovieResponse> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await context.Movies.FirstOrDefaultAsync(x => x.Id == request.Id)
                     ?? throw new ResourceNotFoundException("Movie not foun");

        return mapper.Map<MovieResponse>(result);
    }
}