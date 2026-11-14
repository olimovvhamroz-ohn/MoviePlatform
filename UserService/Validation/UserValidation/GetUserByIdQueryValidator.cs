using FluentValidation;
using UserService.CQRS.User.Query;

namespace UserService.Validation.UserValidation;

public class GetUserByIdQueryValidator: AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}