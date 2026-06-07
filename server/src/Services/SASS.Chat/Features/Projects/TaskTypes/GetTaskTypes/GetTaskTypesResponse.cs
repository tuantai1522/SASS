namespace SASS.Chat.Features.Projects.TaskTypes.GetTaskTypes;

public sealed record GetTaskTypesResponse(Guid Id, string Name, string Key, string ColorToken, string IconKey, int Order);
