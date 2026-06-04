using MediatR;

namespace SASS.Chat.Features.Projects.GetProjects;

public sealed class GetProjectsQuery : IRequest<IReadOnlyList<GetProjectsResponse>>
{

}
