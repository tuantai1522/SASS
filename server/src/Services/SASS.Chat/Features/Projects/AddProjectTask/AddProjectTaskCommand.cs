using MediatR;

namespace SASS.Chat.Features.Projects.AddProjectTask;

public sealed record AddProjectTaskCommand : IRequest<IdResult>
{
    public Guid ProjectId { get; init; }
    public string Title { get; init; } = null!;
    public string? Description { get; init; }
    public Guid StatusId { get; init; }
    public Guid PriorityId { get; init; }
    public Guid? AssigneeId { get; init; }
    public DateOnly? StartDate { get; init; }
    public DateOnly? DueDate { get; init; }
}
