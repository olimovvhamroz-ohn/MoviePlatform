using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviePlatform.Data;
using MoviePlatform.DTO.Response;

namespace MoviePlatform.CQRS.Movie.Query;
public record GetAllMovieQuery(int Page,
    int PageSize
    ,long? CategoryId
    ,long? StudioId) : IRequest<List<MovieResponse>>;

public class GetAllMovieQueryHandler(
    ApplicationContext context,
    IMapper mapper) : IRequestHandler<GetAllMovieQuery, List<MovieResponse>>
{
    public async Task<List<MovieResponse>> Handle(
        GetAllMovieQuery request, CancellationToken cancellationToken)
    {
        var page = Math.Max(request.Page, 1);
        var pageSize = Math.Clamp(request.PageSize, 1, 100);

        var query = context.Movies
            .AsNoTracking()
            .Include(x => x.Category)
            .Include(x => x.Studio)
            .Include(x => x.Actors)
            .AsQueryable();
        if (request.CategoryId.HasValue)
            query = query.Where(x => x.CategoryId == request.CategoryId);

        if (request.StudioId.HasValue)
            query = query.Where(x => x.StudioId == request.StudioId);

        var movies = await query
            .OrderBy(x => x.Title)
            .ThenBy(x => x.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        return mapper.Map<List<MovieResponse>>(movies);
    }
}