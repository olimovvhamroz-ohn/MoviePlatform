using FluentValidation;
using UserService.DTO.Request;

namespace UserService.Validation.UserValidation;

public class UpdateUserCommandValidator : AbstractValidator<RegisterRequest>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(150);
        RuleFor(x => x.BirthDate).LessThan(_ => DateOnly.FromDateTime(DateTime.UtcNow));
    }
}