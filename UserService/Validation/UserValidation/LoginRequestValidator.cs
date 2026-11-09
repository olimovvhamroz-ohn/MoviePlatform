using FluentValidation;
using UserService.DTO.Request;

namespace UserService.Validation.UserValidation;

public class LoginRequestValidator: AbstractValidator<RegisterRequest>
{
    public LoginRequestValidator()
    {

        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();

    }
}