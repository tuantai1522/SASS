namespace SASS.Chat.Features.Projects.GetProjects;

public sealed record GetProjectsResponse(
    Guid Id,
    string Code,
    string Title);
