namespace SASS.Chat.Domain.AggregatesModel.Projects;

public sealed class Project : Entity, IAggregateRoot, ISoftDelete
{
    private readonly List<ProjectMember> _members = [];
    private readonly List<Task> _tasks = [];

    private Project()
    {
    }

    public static Project Create(
        Guid ownerId,
        string code,
        string title,
        string? description,
        IReadOnlyList<Guid> memberIds,
        IReadOnlyList<Guid> leaderIds)
    {
        var project = new Project
        {
            OwnerId = ownerId,
            Code = code,
            Title = title,
            Description = description,
            NextTaskSequence = 1,
            CreatedAt = DateTimeOffset.Now.ToUnixTimeSeconds(),
        };

        project.AddMembers(memberIds.Select(memberId => ProjectMember.Create(project.Id, memberId, ProjectMemberRole.Member)).ToList());
        project.AddMembers(leaderIds.Select(memberId => ProjectMember.Create(project.Id, memberId, ProjectMemberRole.Leader)).ToList());

        return project;
    }

    public Guid OwnerId { get; private init; }
    public User Owner { get; private set; } = null!;

    public string Code { get; private set; } = null!;

    public string Title { get; private set; } = null!;
    public string? Description { get; private set; }

    public int NextTaskSequence { get; private set; }

    public long CreatedAt { get; private init; }

    public bool IsDeleted { get; set; }

    public IReadOnlyCollection<ProjectMember> Members => _members;
    public IReadOnlyCollection<Task> Tasks => _tasks;

    public void ChangeName(string name)
    {
        Title = name;
    }

    public void ChangeDescription(string? description)
    {
        Description = description;
    }

    public void ChangeCode(string code)
    {
        Code = code;
    }

    public int GetNextTaskCode()
    {
        var code = NextTaskSequence;
        NextTaskSequence++;
        return code;
    }

    public void AddMembers(IReadOnlyList<ProjectMember> members)
    {
        _members.AddRange(members);
    }

    public void AddTask(Task task)
    {
        _tasks.Add(task);
    }

    public void Delete()
    {
        IsDeleted = true;

        foreach (var member in _members.Where(x => !x.IsDeleted))
        {
            member.Delete();
        }

        foreach (var task in _tasks.Where(x => !x.IsDeleted))
        {
            task.Delete();
        }
    }
}
