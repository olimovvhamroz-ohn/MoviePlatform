using FluentValidation;
using UserService.SQRS.User.Command;

namespace UserService.Validation.UserValidation;
public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}