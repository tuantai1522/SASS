using FluentValidation;

namespace SASS.Chat.Features.Projects.DeleteProject;

internal sealed class DeleteProjectCommandValidator : AbstractValidator<DeleteProjectCommand>
{
    public DeleteProjectCommandValidator()
    {
        RuleFor(x => x.ProjectId)
            .NotEmpty();
    }
}
