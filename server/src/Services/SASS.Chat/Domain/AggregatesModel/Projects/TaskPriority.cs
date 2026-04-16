namespace SASS.Chat.Domain.AggregatesModel.Projects;

public sealed class TaskPriority : Entity
{
    private TaskPriority()
    {
    }

    public string Name { get; private init; } = null!;
    
    public int Order { get; private init; }
}
