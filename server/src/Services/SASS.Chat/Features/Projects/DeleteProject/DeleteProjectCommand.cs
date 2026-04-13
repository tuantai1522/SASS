using MediatR;

namespace SASS.Chat.Features.Projects.DeleteProject;

public sealed record DeleteProjectCommand(Guid ProjectId) : IRequest<IdResult>;
