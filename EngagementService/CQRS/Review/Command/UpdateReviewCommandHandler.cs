using EngagementService.Data;
using EngagementService.DTO.Request;
using EngagementService.DTO.Response;
using EngagementService.Exception;
using EngagementService.Mapping;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts;

namespace EngagementService.CQRS.Review.Command;

public record UpdateReviewCommand(
    UpdateReviewRequest Request,
    long UserId
) : IRequest<ReviewResponse>;

public class UpdateReviewCommandHandler(
    EngagementDbContext context,
    IMovieService service)
    : IRequestHandler<UpdateReviewCommand, ReviewResponse>
{
    public async Task<ReviewResponse> Handle(
        UpdateReviewCommand request,
        CancellationToken cancellationToken)
    { var movie = await service.GetMovieById(
                 request.Request.MovieId);
     
             if (movie is null)
                 throw new ResourceNotFoundException("Movie not found.");
        
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