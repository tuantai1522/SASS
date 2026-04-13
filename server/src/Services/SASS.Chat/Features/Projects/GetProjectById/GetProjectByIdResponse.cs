namespace SASS.Chat.Features.Projects.GetProjectById;

public sealed class GetProjectByIdResponse
{
    public Guid Id { get; init; }
    public string Code { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public long CreatedAt { get; init; }
    public Guid OwnerId { get; init; }
}
