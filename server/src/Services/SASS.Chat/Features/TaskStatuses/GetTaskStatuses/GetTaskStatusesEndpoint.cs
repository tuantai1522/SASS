using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using TaskStatus = SASS.Chat.Domain.AggregatesModel.Projects.TaskStatus;

namespace SASS.Chat.Features.TaskStatuses.GetTaskStatuses;

public sealed class GetTaskStatusesEndpoint : IEndpoint<Ok<IReadOnlyList<GetTaskStatusesResponse>>, ISender, GetTaskStatusesQuery>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("task-statuses", HandleAsync)
            .WithTags(nameof(TaskStatus))
            .WithName(nameof(GetTaskStatusesEndpoint))
            .WithDescription("Get task statuses")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization()
            .Produces<IReadOnlyList<GetTaskStatusesResponse>>();
    }

    public async Task<Ok<IReadOnlyList<GetTaskStatusesResponse>>> HandleAsync(
        ISender sender,
        [AsParameters] GetTaskStatusesQuery query,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(query, cancellationToken);
        return TypedResults.Ok(result);
    }
}
