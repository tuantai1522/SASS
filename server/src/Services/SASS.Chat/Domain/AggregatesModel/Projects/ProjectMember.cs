namespace SASS.Chat.Domain.AggregatesModel.Projects;

public sealed class ProjectMember : ISoftDelete
{
    private ProjectMember()
    {
    }

    public static ProjectMember Create(Guid projectId, Guid userId, ProjectMemberRole role)
    {
        return new ProjectMember
        {
            ProjectId = projectId,
            UserId = userId,
            Role = role,
            JoinedAt = DateTimeOffset.Now.ToUnixTimeSeconds()
        };
    }

    public Guid ProjectId { get; private init; }
    public Project Project { get; private set; } = null!;

    public Guid UserId { get; private init; }
    public User User { get; private set; } = null!;

    public ProjectMemberRole Role { get; private set; }
    public long JoinedAt { get; private init; }

    public bool IsDeleted { get; set; }

    public void ChangeRole(ProjectMemberRole role)
    {
        Role = role;
    }

    public void Delete()
    {
        IsDeleted = true;
    }
}
