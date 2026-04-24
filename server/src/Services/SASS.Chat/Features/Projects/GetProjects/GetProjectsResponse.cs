namespace SASS.Chat.Features.Projects.GetProjects;

public sealed class GetProjectsResponse
{
    public IReadOnlyList<GetProjectsItemResponse> Items { get; init; } = [];
    public int PageIndex { get; init; }
    public int PageSize { get; init; }
    public long TotalItems { get; init; }
    public long TotalPages { get; init; }
    public bool HasPreviousPage { get; init; }
    public bool HasNextPage { get; init; }
}
