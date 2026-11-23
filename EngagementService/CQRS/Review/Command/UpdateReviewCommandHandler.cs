using EngagementService.Data;
using EngagementService.DTO.Request;
using EngagementService.DTO.Response;
using EngagementService.Exception;
using EngagementService.Mapping;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EngagementService.CQRS.Review.Command;

public record UpdateReviewCommand(
    UpdateReviewRequest Request,
    long UserId
) : IRequest<ReviewResponse>;

public class UpdateReviewCommandHandler(
    EngagementDbContext context)
    : IRequestHandler<UpdateReviewCommand, ReviewResponse>
{
    public async Task<ReviewResponse> Handle(
        UpdateReviewCommand request,
        CancellationToken cancellationToken)
    {
        var review = await context.Reviews.FirstOrDefaultAsync(
            x => x.UserId == request.UserId &&
                 x.MovieId == request.Request.MovieId,
            cancellationToken);

        if (review == null)
        {
            throw new ResourceNotFoundException("Review not found");
        }

        review.Rating = request.Request.Rating;
        review.Comment = request.Request.Comment;

        await context.SaveChangesAsync(cancellationToken);

        return ReviewMapping.ToResponse(review);
    }
}