using FluentValidation;
using UserService.CQRS.User.Command;

namespace UserService.Validation.UserValidation;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Request.Name).NotEmpty().MaximumLength(150);
        RuleFor(x => x.Request.Email).NotEmpty().EmailAddress().MaximumLength(256);
        RuleFor(x => x.Request.Password).NotEmpty().MinimumLength(8).MaximumLength(100);
        RuleFor(x => x.Request.BirthDate).LessThan(_ => DateOnly.FromDateTime(DateTime.UtcNow));
    }
}