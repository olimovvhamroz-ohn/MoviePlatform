using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviePlatform.Data;
using MoviePlatform.DTO.Request;
using MoviePlatform.DTO.Response;
using MoviePlatform.Entities;
using MoviePlatform.Exceptions;

namespace MoviePlatform.CQRS.Review.Command;

public record CreateReviewCommand(ReviewRequest Review) : IRequest<ReviewResponse>;

public class CreateReviewCommandHandler(
    ApplicationContext context,
    IMapper mapper,
    ILogger<CreateReviewCommandHandler> logger,
    IValidator<ReviewRequest> validator) : IRequestHandler<CreateReviewCommand, ReviewResponse>
{
    public async Task<ReviewResponse> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var validation = await validator.ValidateAsync(request.Review, cancellationToken);

        if (!validation.IsValid)
        {
            throw new BadRequestException(
                string.Join("; ", validation.Errors.Select(e => e.ErrorMessage)));
        }

        var isDuplicate = await context.Reviews.AnyAsync(
            x => x.UserId == request.Review.UserId && x.MovieId == request.Review.MovieId, cancellationToken);
        
        if (isDuplicate)
        {
            throw new ResourceIsAlreadyExistException("You have already reviewed this movie");
        }

        var review = mapper.Map<ReviewEntity>(request.Review);

        context.Reviews.Add(review);
        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Review {Id} created for movie {MovieId}", review.Id, review.MovieId);
        return mapper.Map<ReviewResponse>(review);
    }
}