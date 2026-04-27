namespace SASS.Chat.Features.Projects.GetProjects;

public sealed class GetProjectsItemResponse(
    Guid Id,
    string Code,
    string Title,
    string? Description,
    long CreatedAt,
    string Role,
    int Progress);
