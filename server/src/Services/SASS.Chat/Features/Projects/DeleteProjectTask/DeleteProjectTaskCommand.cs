using MediatR;

namespace SASS.Chat.Features.Projects.DeleteProjectTask;

public sealed record DeleteProjectTaskCommand(Guid ProjectId, Guid TaskId) : IRequest<IdResult>;
