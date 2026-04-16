using MediatR;

namespace SASS.Chat.Features.TaskPriorities.GetTaskPriorities;

public sealed record GetTaskPrioritiesQuery : IRequest<IReadOnlyList<GetTaskPrioritiesResponse>>;

