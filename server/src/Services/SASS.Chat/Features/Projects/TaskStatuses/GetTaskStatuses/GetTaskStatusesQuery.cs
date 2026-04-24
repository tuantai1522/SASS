using MediatR;

namespace SASS.Chat.Features.Projects.TaskStatuses.GetTaskStatuses;

public sealed class GetTaskStatusesQuery : IRequest<IReadOnlyList<GetTaskStatusesResponse>>
{

}
