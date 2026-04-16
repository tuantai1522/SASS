using FluentValidation;

namespace SASS.Chat.Features.Projects.AddProjectTask;

internal sealed class AddProjectTaskCommandValidator : AbstractValidator<AddProjectTaskCommand>
{
    public AddProjectTaskCommandValidator()
    {
        RuleFor(x => x.ProjectId)
            .NotEmpty();

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(512);

        RuleFor(x => x.StatusId)
            .NotEmpty();

        RuleFor(x => x.PriorityId)
            .NotEmpty();

        RuleFor(x => x)
            .Must(x => x.StartDate is null || x.DueDate is null || x.StartDate <= x.DueDate)
            .WithMessage("StartDate must be less than or equal to DueDate.");
    }
}
