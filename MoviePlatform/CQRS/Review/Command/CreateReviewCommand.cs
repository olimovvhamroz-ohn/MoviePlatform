using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviePlatform.Data;
using MoviePlatform.DTO.Request;
using MoviePlatform.DTO.Response;
using MoviePlatform.Exceptions;
using MoviePlatform.Mapping;

namespace MoviePlatform.CQRS.Review.Command;

public record CreateReviewCommand(ReviewRequest Review) : IRequest<ReviewResponse>;

public class CreateReviewCommandHandler(
    ApplicationContext context,
    ILogger<CreateReviewCommandHandler> logger,
    IValidator<ReviewRequest> validator) : IRequestHandler<CreateReviewCommand, ReviewResponse>
{
    public async Task<ReviewResponse> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var isDuplicate = await context.Reviews.AnyAsync(
            x => x.UserId == request.Review.UserId && x.MovieId == request.Review.MovieId, cancellationToken);

        if (isDuplicate)
        {
            throw new ResourceIsAlreadyExistException("You have already reviewed this movie");
        }

        var review = ReviewMapping.ToEntity(request.Review);
        context.Reviews.Add(review);
        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Review {Id} created for movie {MovieId}", review.Id, review.MovieId);
        return ReviewMapping.ToResponse(review);
    }
}