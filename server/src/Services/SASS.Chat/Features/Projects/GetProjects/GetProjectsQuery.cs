using MediatR;

namespace SASS.Chat.Features.Projects.GetProjects;

public sealed class GetProjectsQuery : PagedRequest, IRequest<PagedResult<GetProjectsItemResponse>>
{
    /// <summary>
    /// This can be used to search title
    /// </summary>
    public string? Search { get; init; }
    
    /// <summary>
    /// Order by field CreatedAt, Title
    /// </summary>
    public ProjectOrderBy OrderBy { get; init; } = ProjectOrderBy.CreatedAt;
}
