using MediatR;

namespace SASS.Chat.Features.Projects.GetProjectById;

public sealed record GetProjectByIdQuery(Guid ProjectId) : IRequest<GetProjectByIdResponse>;
