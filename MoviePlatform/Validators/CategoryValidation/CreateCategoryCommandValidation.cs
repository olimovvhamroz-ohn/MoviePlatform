using FluentValidation;
using MoviePlatform.CQRS.Category.Command;

namespace MoviePlatform.Validators.CategoryValidation;

public class CreateCategoryCommandValidation : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidation()
    {
        RuleFor(x => x.Request.Title).NotEmpty().MaximumLength(50);
    }
}