namespace SASS.Chat.Features.Projects.GetProjects;

public sealed class GetProjectsItemResponse
{
    public Guid Id { get; init; }
    public string Code { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public long CreatedAt { get; init; }
    public string Role { get; init; } = string.Empty;
    public int Progress { get; init; }
}
