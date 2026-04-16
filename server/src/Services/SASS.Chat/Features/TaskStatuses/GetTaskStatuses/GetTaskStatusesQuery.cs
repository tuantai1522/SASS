using MediatR;

namespace SASS.Chat.Features.TaskStatuses.GetTaskStatuses;

public sealed class GetTaskStatusesQuery : IRequest<IReadOnlyList<GetTaskStatusesResponse>>
{

}
