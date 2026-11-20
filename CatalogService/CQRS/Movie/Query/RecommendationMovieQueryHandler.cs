using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviePlatform.Data;
using MoviePlatform.DTO.Response;
using MoviePlatform.Entities.Enums;
using MoviePlatform.Grpc;

namespace MoviePlatform.CQRS.Movie.Query;

public record RecommendationMovieQuery(long UserId)
    : IRequest<List<MovieResponse>>;

public class RecommendationMovieQueryHandler(
    UserGrpcClient userGrpcClient,
    ApplicationContext context
) : IRequestHandler<RecommendationMovieQuery, List<MovieResponse>>
{
    public async Task<List<MovieResponse>> Handle(
        RecommendationMovieQuery request,
        CancellationToken cancellationToken)
    {
        var user = await userGrpcClient.GetUserById(request.UserId);
        if (user is null)
            throw new UnauthorizedAccessException("User not found.");

        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var age = today.Year - user.BirthDate.Year;

        if (user.BirthDate > today.AddYears(-age))
            age--;

        var movies = await context.Movies
            .AsNoTracking()
            .Include(m => m.Category)
            .Include(m => m.Actors)
            .Include(m => m.Studio)
            .ToListAsync(cancellationToken);
        
        var recommendedMovies = movies
            .Where(movie =>
                movie.AgeRating == AgeRating.Default ||
                (movie.AgeRating == AgeRating.SixPlus && age >= 6) ||
                (movie.AgeRating == AgeRating.TwelvePlus && age >= 12) ||
                (movie.AgeRating == AgeRating.SixteenPlus && age >= 16) ||
                (movie.AgeRating == AgeRating.EighteenPlus && age >= 18))
            .ToList();

        return recommendedMovies.Select(movie => new MovieResponse
        {
            Title = movie.Title,
            Price = movie.Price,
            Description = movie.Description,
            DurationInMinutes = movie.DurationInMinutes,
            ReleaseYear = movie.ReleaseYear,
            AverageRating = (double)movie.AverageRating,
            ReviewsCount = movie.ReviewsCount,
            AgeRating = movie.AgeRating,
            PosterUrl = movie.PosterUrl,
            CategoryId = movie.CategoryId,
            Category = new CategoryResponse
            {
                Title = movie.Category.Title
            },
            Actors = new List<ActorResponse>(),
            StudioId = movie.StudioId,
            Studio = new StudioResponse
            {
                Name = movie.Studio.Name
            }
        }).ToList();
    }
}