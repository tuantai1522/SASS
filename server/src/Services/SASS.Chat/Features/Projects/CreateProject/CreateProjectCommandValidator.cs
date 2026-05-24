using FluentValidation;

namespace SASS.Chat.Features.Projects.CreateProject;

internal sealed class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(64);

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(512);
    }
}
