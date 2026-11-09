using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviePlatform.Data;
using MoviePlatform.DTO.Response;

namespace MoviePlatform.CQRS.Review.Query;

public record GetReviewsByMovieQuery(long MovieId, int Page, int PageSize)
    : IRequest<List<ReviewResponse>>;

public class GetReviewsByMovieQueryHandler(
    ApplicationContext context,
    IMapper mapper) : IRequestHandler<GetReviewsByMovieQuery, List<ReviewResponse>>
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

        return mapper.Map<List<ReviewResponse>>(reviews);
    }
}