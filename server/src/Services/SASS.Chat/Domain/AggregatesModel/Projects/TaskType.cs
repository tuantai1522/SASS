namespace SASS.Chat.Domain.AggregatesModel.Projects;

public sealed class TaskType : Entity
{
    private TaskType()
    {
    }

    public string Name { get; private init; } = null!;

    public string Key { get; private init; } = null!;

    public string ColorToken { get; private init; } = null!;

    public string IconKey { get; private init; } = null!;

    public int Order { get; private init; }
}
