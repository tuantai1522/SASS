namespace SASS.Chat.Features.Projects.TaskStatuses.GetTaskStatuses;

public sealed record GetTaskStatusesResponse(Guid Id, string Name, string Key, string ColorToken, string IconKey, int Order);
