using EngagementService.Data;
using EngagementService.Exception;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EngagementService.CQRS.Review.Command;

public record DeleteReviewCommand(
    long MovieId,
    long UserId
) : IRequest<bool>;

public class DeleteReviewCommandHandler(
    EngagementDbContext context)
    : IRequestHandler<DeleteReviewCommand, bool>
{
    public async Task<bool> Handle(
        DeleteReviewCommand request,
        CancellationToken cancellationToken)
    {
        var review = await context.Reviews.FirstOrDefaultAsync(
            x => x.UserId == request.UserId &&
                 x.MovieId == request.MovieId,
            cancellationToken);

        if (review == null)
        {
            throw new ResourceNotFoundException("Review not found");
        }

        context.Reviews.Remove(review);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}