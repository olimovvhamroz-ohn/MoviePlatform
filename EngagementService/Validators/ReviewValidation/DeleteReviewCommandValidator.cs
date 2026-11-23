using EngagementService.CQRS.Review.Command;
using FluentValidation;

namespace EngagementService.Validators.ReviewValidation;

public class DeleteReviewCommandValidator
    : AbstractValidator<DeleteReviewCommand>
{
    public DeleteReviewCommandValidator()
    {
        RuleFor(x => x.MovieId)
            .GreaterThan(0)
            .WithMessage("MovieId must be greater than 0");

        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("UserId must be greater than 0");
    }
}