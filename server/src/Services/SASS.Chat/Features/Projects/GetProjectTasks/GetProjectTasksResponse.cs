namespace SASS.Chat.Features.Projects.GetProjectTasks;

public sealed class GetProjectTasksResponse
{
    public Guid Id { get; init; }
    public string Code { get; init; } = null!;
    public string Title { get; init; } = null!;
    
    public Guid StatusId { get; init; }
    public string StatusName { get; init; } = null!;
    public string StatusKey { get; init; } = null!;
    public string StatusColorToken { get; init; } = null!;
    public string StatusIconKey { get; init; } = null!;
    
    public Guid PriorityId { get; init; }
    public string PriorityName { get; init; } = null!;
    public string PriorityKey { get; init; } = null!;
    public string PriorityColorToken { get; init; } = null!;
    public string PriorityIconKey { get; init; } = null!;

    public Guid TypeId { get; init; }
    public string TypeName { get; init; } = null!;
    public string TypeKey { get; init; } = null!;
    public string TypeColorToken { get; init; } = null!;
    public string TypeIconKey { get; init; } = null!;
    
    /// <summary>
    /// Assignee can be null
    /// </summary>
    public Guid? AssigneeId { get; init; }
    public string? AssigneeName { get; init; }
    
    public DateOnly? StartDate { get; init; }
    public DateOnly? DueDate { get; init; }

    public long CreatedAt { get; init; }
}
