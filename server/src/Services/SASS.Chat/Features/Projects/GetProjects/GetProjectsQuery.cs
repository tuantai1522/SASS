using System.Text.Json.Serialization;
using MediatR;

namespace SASS.Chat.Features.Projects.GetProjects;

public sealed class GetProjectsQuery : PagedRequest, IRequest<GetProjectsResponse>
{
    /// <summary>
    /// This can be used to search title
    /// </summary>
    public string? Search { get; init; }

    /// <summary>
    /// Order by field CreatedAt, Title
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<ProjectOrderBy>))]
    public ProjectOrderBy OrderBy { get; init; } = ProjectOrderBy.CreatedAt;
}
