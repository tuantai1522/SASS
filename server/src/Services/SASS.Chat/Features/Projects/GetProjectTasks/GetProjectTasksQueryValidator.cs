using FluentValidation;

namespace SASS.Chat.Features.Projects.GetProjectTasks;

internal sealed class GetProjectTasksQueryValidator : AbstractValidator<GetProjectTasksQuery>
{
    public GetProjectTasksQueryValidator()
    {
        RuleFor(x => x.ProjectId)
            .NotEmpty();

        RuleForEach(x => x.StatusIds)
            .NotNull();

        RuleForEach(x => x.AssigneeIds)
            .NotNull();

        RuleForEach(x => x.PriorityIds)
            .NotNull();
    }
}
