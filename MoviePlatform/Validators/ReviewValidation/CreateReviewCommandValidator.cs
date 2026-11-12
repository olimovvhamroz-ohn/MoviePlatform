using FluentValidation;
using MoviePlatform.CQRS.Review.Command;

namespace MoviePlatform.Validators.ReviewValidation;

public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(x => x.Review.UserId).GreaterThan(0);
        RuleFor(x => x.Review.MovieId).GreaterThan(0);
        RuleFor(x => x.Review.Rating)
            .InclusiveBetween(1, 10)
            .WithMessage("Rating must be between 1 and 10");
        RuleFor(x => x.Review.Comment).MaximumLength(2000);
    }
}