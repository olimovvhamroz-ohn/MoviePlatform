using FluentValidation;
using MoviePlatform.CQRS.Studio.Command;

namespace MoviePlatform.Validators.StudioValidation;

public class DeleteStudioCommandValidatior : AbstractValidator<DeleteStudioCommand>
{
    public DeleteStudioCommandValidatior()
    {
        RuleFor(x => x.Id).NotNull().GreaterThan(0);
    }
}