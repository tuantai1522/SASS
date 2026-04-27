namespace SASS.Chat.Features.Projects.GetProjects;

public sealed record GetProjectsItemResponse(
    Guid Id,
    string Code,
    string Title,
    string? Description,
    long CreatedAt,
    string Role,
    int Progress);
