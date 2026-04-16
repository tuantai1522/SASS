using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace SASS.Chat.Features.Projects.GetProjectTasks;

public sealed class GetProjectTasksEndpoint : IEndpoint<Ok<PagedResult<GetProjectTasksItemResponse>>, Guid, GetProjectTasksQuery, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("projects/{projectId:guid}/tasks/search", HandleAsync)
            .WithTags(nameof(Project))
            .WithName(nameof(GetProjectTasksEndpoint))
            .WithDescription("Get project tasks with normal pagination")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization()
            .Produces<PagedResult<GetProjectTasksItemResponse>>();
    }

    public async Task<Ok<PagedResult<GetProjectTasksItemResponse>>> HandleAsync(
        Guid projectId,
        [FromBody] GetProjectTasksQuery query,
        ISender sender,
        CancellationToken cancellationToken = default)
    {
        var request = new GetProjectTasksQuery
        {
            ProjectId = projectId,
            Page = query.Page,
            PageSize = query.PageSize,
            Order = query.Order,
            OrderBy = query.OrderBy,
            StatusIds = query.StatusIds,
            AssigneeIds = query.AssigneeIds,
            PriorityIds = query.PriorityIds,
            Search = query.Search,
        };
        
        var result = await sender.Send(request, cancellationToken);
        
        return TypedResults.Ok(result);
    }
}
