using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace SASS.Chat.Features.TaskPriorities.GetTaskPriorities;

public sealed class GetTaskPrioritiesEndpoint : IEndpoint<Ok<IReadOnlyList<GetTaskPrioritiesResponse>>, ISender, GetTaskPrioritiesQuery>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("task-priorities", HandleAsync)
            .WithTags(nameof(TaskPriority))
            .WithName(nameof(GetTaskPrioritiesEndpoint))
            .WithDescription("Get task priorities")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization()
            .Produces<IReadOnlyList<GetTaskPrioritiesResponse>>();
    }

    public async Task<Ok<IReadOnlyList<GetTaskPrioritiesResponse>>> HandleAsync(
        ISender sender,
        [AsParameters] GetTaskPrioritiesQuery query,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(query, cancellationToken);
        return TypedResults.Ok(result);
    }
}
