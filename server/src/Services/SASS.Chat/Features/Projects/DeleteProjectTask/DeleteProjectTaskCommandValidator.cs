using FluentValidation;

namespace SASS.Chat.Features.Projects.DeleteProjectTask;

public sealed class DeleteProjectTaskCommandValidator : AbstractValidator<DeleteProjectTaskCommand>
{
    public DeleteProjectTaskCommandValidator()
    {
        RuleFor(x => x.ProjectId)
            .NotEmpty();

        RuleFor(x => x.TaskId)
            .NotEmpty();
    }
}
