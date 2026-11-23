using EngagementService.CQRS.Review.Command;
using FluentValidation;

namespace EngagementService.Validators.ReviewValidation;

public class UpdateReviewCommandValidator
    : AbstractValidator<UpdateReviewCommand>
{
    public UpdateReviewCommandValidator()
    {
        RuleFor(x => x.Request.MovieId)
            .GreaterThan(0)
            .WithMessage("MovieId must be greater than 0");

        RuleFor(x => x.Request.Rating)
            .InclusiveBetween(1, 10)
            .WithMessage("Rating must be between 1 and 10");

        RuleFor(x => x.Request.Comment)
            .NotEmpty()
            .WithMessage("Comment is required")
            .MaximumLength(1000)
            .WithMessage("Comment cannot exceed 1000 characters");

        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("UserId must be greater than 0");
    }
}