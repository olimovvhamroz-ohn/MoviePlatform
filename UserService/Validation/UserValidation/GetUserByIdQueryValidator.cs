using FluentValidation;
using UserService.SQRS.User.Query;

namespace UserService.Validation.UserValidation;

public class GetUserByIdQueryValidator: AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}