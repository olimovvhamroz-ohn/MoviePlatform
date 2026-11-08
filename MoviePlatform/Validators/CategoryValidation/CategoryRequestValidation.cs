using FluentValidation;
using MoviePlatform.DTO.Request;

namespace MoviePlatform.Validators.CategoryValidation;

public class CategoryRequestValidation:AbstractValidator<CategoryRequest>
{
    public CategoryRequestValidation()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(50);
    }
}