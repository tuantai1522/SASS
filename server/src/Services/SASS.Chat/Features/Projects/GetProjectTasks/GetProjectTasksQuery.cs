using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SASS.Chat.Features.Projects.GetProjectTasks;

public sealed class GetProjectTasksQuery : PagedRequest, IRequest<PagedResult<GetProjectTasksItemResponse>>
{
    public Guid ProjectId { get; init; }

    public IReadOnlyList<Guid> StatusIds { get; init; } = [];

    public IReadOnlyList<Guid> AssigneeIds { get; init; } = [];

    public IReadOnlyList<Guid> PriorityIds { get; init; } = [];

    public string? Search { get; init; }

    [JsonConverter(typeof(JsonStringEnumConverter<GetProjectTasksOrderBy>))]
    public GetProjectTasksOrderBy OrderBy { get; init; } = GetProjectTasksOrderBy.DueDate;
}
