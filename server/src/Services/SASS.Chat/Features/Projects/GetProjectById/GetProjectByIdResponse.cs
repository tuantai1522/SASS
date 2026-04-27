namespace SASS.Chat.Features.Projects.GetProjectById;

public sealed record GetProjectByIdResponse(
    Guid Id,
    string Code,
    string Title,
    string? Description,
    long CreatedAt,
    string Role,
    int Progress,
    int TotalTasks,
    int TotalCompletedTasks);
