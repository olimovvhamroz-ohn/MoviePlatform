using FluentValidation;
using MoviePlatform.DTO.Request;

namespace MoviePlatform.Validators.ReviewValidation;

public class ReviewRequestValidator : AbstractValidator<ReviewRequest>
{
    public ReviewRequestValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0);
        RuleFor(x => x.MovieId).GreaterThan(0);
        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 10)
            .WithMessage("Rating must be between 1 and 10");
        RuleFor(x => x.Comment).MaximumLength(2000);
    }
}