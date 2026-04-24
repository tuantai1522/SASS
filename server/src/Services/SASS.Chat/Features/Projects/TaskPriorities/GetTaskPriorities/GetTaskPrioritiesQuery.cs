using MediatR;

namespace SASS.Chat.Features.Projects.TaskPriorities.GetTaskPriorities;

public sealed record GetTaskPrioritiesQuery : IRequest<IReadOnlyList<GetTaskPrioritiesResponse>>;

