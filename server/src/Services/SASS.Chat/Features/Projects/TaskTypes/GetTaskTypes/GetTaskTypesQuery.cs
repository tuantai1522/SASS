using MediatR;

namespace SASS.Chat.Features.Projects.TaskTypes.GetTaskTypes;

public sealed record GetTaskTypesQuery : IRequest<IReadOnlyList<GetTaskTypesResponse>>;
