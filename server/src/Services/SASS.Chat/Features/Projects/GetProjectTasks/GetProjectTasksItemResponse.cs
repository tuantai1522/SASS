namespace SASS.Chat.Features.Projects.GetProjectTasks;

public sealed class GetProjectTasksItemResponse
{
    public Guid Id { get; init; }
    public string Code { get; init; } = null!;
    public string Title { get; init; } = null!;
    public string? Description { get; init; }
    
    public Guid StatusId { get; init; }
    public string StatusName { get; init; } = null!;
    
    public Guid PriorityId { get; init; }
    public string PriorityName { get; init; } = null!;
    
    /// <summary>
    /// Assignee can be null
    /// </summary>
    public Guid? AssigneeId { get; init; }
    public string? AssigneeName { get; init; }
    
    public DateOnly? StartDate { get; init; }
    public DateOnly? DueDate { get; init; }
    public long CreatedAt { get; init; }
}
