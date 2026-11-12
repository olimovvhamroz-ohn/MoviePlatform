using FluentValidation;
using MoviePlatform.CQRS.Category.Command;

namespace MoviePlatform.Validators.CategoryValidation;

public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().GreaterThan(0);
    }
}