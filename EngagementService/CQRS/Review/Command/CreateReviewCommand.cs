using EngagementService.Data;
using EngagementService.DTO.Request;
using EngagementService.DTO.Response;
using EngagementService.Exception;
using EngagementService.Mapping;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts;

public record CreateReviewCommand(ReviewRequest Review, long UserId)
    : IRequest<ReviewResponse>;

public class CreateReviewCommandHandler(
    EngagementDbContext context,
    ILogger<CreateReviewCommandHandler> logger,
    IMovieService service)
    : IRequestHandler<CreateReviewCommand, ReviewResponse>
{
    public async Task<ReviewResponse> Handle(
        CreateReviewCommand request,
        CancellationToken cancellationToken)
    {
        var movie = await service.GetMovieById(
            request.Review.MovieId);

        if (movie is null)
            throw new ResourceNotFoundException("Movie not found.");
        var isDuplicate = await context.Reviews.AnyAsync(
            x => x.UserId == request.UserId &&
                 x.MovieId == request.Review.MovieId,
            cancellationToken);

        if (isDuplicate)
            throw new ResourceIsAlreadyExistException(
                "You have already reviewed this movie");

        request.Review.UserId = request.UserId;

        var review = ReviewMapping.ToEntity(request.Review);

        context.Reviews.Add(review);
        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation(
            "Review {Id} created for movie {MovieId}",
            review.Id,
            review.MovieId);

        return ReviewMapping.ToResponse(review);
    }
}