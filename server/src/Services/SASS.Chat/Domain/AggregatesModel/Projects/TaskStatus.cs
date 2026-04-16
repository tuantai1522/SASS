namespace SASS.Chat.Domain.AggregatesModel.Projects;

public sealed class TaskStatus : Entity
{
    private TaskStatus()
    {
    }

    public string Name { get; private init; } = null!;
    
    public int Order { get; private init; }
}
