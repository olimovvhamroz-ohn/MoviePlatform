using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviePlatform.Data;
using MoviePlatform.DTO.Response;
using MoviePlatform.Mapping;

namespace MoviePlatform.CQRS.Review.Query;

public record GetReviewsByMovieQuery(long MovieId, int Page, int PageSize)
    : IRequest<List<ReviewResponse>>;

public class GetReviewsByMovieQueryHandler(
    ApplicationContext context) : IRequestHandler<GetReviewsByMovieQuery, List<ReviewResponse>>
{
    public async Task<List<ReviewResponse>> Handle(
        GetReviewsByMovieQuery request, CancellationToken cancellationToken)
    {
        var page = Math.Max(request.Page, 1);
        var pageSize = Math.Clamp(request.PageSize, 1, 100);

        var reviews = await context.Reviews
            .AsNoTracking()
            .Where(x => x.MovieId == request.MovieId)
            .OrderByDescending(x => x.CreatedAt)
            .ThenBy(x => x.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return reviews
            .Select(ReviewMapping.ToResponse)
            .ToList();
    }
}