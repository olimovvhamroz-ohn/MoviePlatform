using FluentValidation;
using UserService.CQRS.User.Command;

namespace UserService.Validation.UserValidation;
public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}