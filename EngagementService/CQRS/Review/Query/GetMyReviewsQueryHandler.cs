using EngagementService.Data;
using EngagementService.DTO.Response;
using EngagementService.Mapping;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EngagementService.CQRS.Review.Query;
public record GetMyReviewsQuery(  
    long UserId,
    int Page,
    int PageSize) : IRequest<List<ReviewResponse>>;
public class GetMyReviewsQueryHandler(EngagementDbContext context)
    : IRequestHandler<GetMyReviewsQuery,List<ReviewResponse>>
{
    public async Task<List<ReviewResponse>> Handle(GetMyReviewsQuery request, CancellationToken cancellationToken)
    {
        var page = Math.Max(request.Page, 1);
        var pageSize = Math.Clamp(request.PageSize, 1, 100);

        var reviews = await context.Reviews
            .AsNoTracking()
            .Where(x => x.UserId == request.UserId)
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