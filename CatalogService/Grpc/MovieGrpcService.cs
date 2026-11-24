using MagicOnion;
using MagicOnion.Server;
using Microsoft.EntityFrameworkCore;
using MoviePlatform.Data;
using Shared.Contracts;
using Shared.DTOs;

namespace MoviePlatform.Grpc;

public class MovieGrpcService(ApplicationContext context) 
    : ServiceBase<IMovieService>, IMovieService
{
    public async UnaryResult<MovieDto?> GetMovieById(long id)
    {
            
        var movie = await context.Movies.AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

        if (movie is null)
            return null;

        var movieDto = new MovieDto()
        {
            Id = movie.Id,
            Title = movie.Title,
            Description = movie.Description,
            AverageRating = movie.AverageRating,
            DurationInMinutes = movie.DurationInMinutes,
            Price = movie.Price,
            CategoryId = movie.CategoryId,
            ReviewsCount = movie.ReviewsCount,
            ReleaseYear = movie.ReleaseYear,
            PosterUrl = movie.PosterUrl,
            StudioId = movie.StudioId,
        };
        return movieDto;
    }

    public async UnaryResult<List<MovieDto>> GetMoviesByIds(HashSet<long> ids)
    {
        var movies = await context.Movies
            .AsNoTracking()
            .Where(m => ids.Contains(m.Id))
            .Select(m => new MovieDto
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                AverageRating = m.AverageRating,
                DurationInMinutes = m.DurationInMinutes,
                Price = m.Price,
                CategoryId = m.CategoryId,
                ReviewsCount = m.ReviewsCount,
                ReleaseYear = m.ReleaseYear,
                PosterUrl = m.PosterUrl,
                StudioId = m.StudioId
            })
            .ToListAsync();

        return movies;
    }
}