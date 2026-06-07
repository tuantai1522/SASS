namespace SASS.Chat.Features.Projects.TaskPriorities.GetTaskPriorities;

public sealed record GetTaskPrioritiesResponse(Guid Id, string Name, string Key, string ColorToken, string IconKey, int Order);
