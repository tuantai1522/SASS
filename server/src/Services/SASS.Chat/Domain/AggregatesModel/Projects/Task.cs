namespace SASS.Chat.Domain.AggregatesModel.Projects;

public sealed class Task : Entity, ISoftDelete
{
    private Task()
    {
    }

    public static Task Create(
        Guid projectId,
        int code,
        string title,
        Guid statusId,
        Guid priorityId,
        Guid? assigneeId,
        DateOnly? startDate,
        DateOnly? dueDate)
    {
        return new Task
        {
            ProjectId = projectId,
            Code = code,
            Title = title,
            StatusId = statusId,
            PriorityId = priorityId,
            AssigneeId = assigneeId,
            StartDate = startDate,
            DueDate = dueDate,
            CreatedAt = DateTimeOffset.Now.ToUnixTimeSeconds()
        };
    }

    public Guid ProjectId { get; private init; }
    public Project Project { get; private set; } = null!;

    public int Code { get; private init; }

    public string Title { get; private set; } = null!;
    public string? Description { get; private set; }

    public Guid? AssigneeId { get; private set; }
    public User? Assignee { get; private set; }

    public Guid StatusId { get; private set; }
    public TaskStatus Status { get; private set; } = null!;

    public Guid PriorityId { get; private set; }
    public TaskPriority Priority { get; private set; } = null!;

    public DateOnly? StartDate { get; private set; }
    public DateOnly? DueDate { get; private set; }
    public long CreatedAt { get; private init; }

    public bool IsDeleted { get; set; }

    public void Assign(Guid? assigneeId)
    {
        AssigneeId = assigneeId;
    }

    public void ChangePriority(Guid priorityId)
    {
        PriorityId = priorityId;
    }

    public void ChangeStatus(Guid statusId)
    {
        StatusId = statusId;
    }

    public void Rename(string title)
    {
        Title = title;
    }

    public void Delete()
    {
        IsDeleted = true;
    }
}
