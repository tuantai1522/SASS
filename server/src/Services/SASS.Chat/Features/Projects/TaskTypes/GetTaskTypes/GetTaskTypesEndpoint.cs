using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using TaskType = SASS.Chat.Domain.AggregatesModel.Projects.TaskType;

namespace SASS.Chat.Features.Projects.TaskTypes.GetTaskTypes;

public sealed class GetTaskTypesEndpoint : IEndpoint<Ok<IReadOnlyList<GetTaskTypesResponse>>, ISender, GetTaskTypesQuery>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("task-types", HandleAsync)
            .WithTags(nameof(TaskType))
            .WithName(nameof(GetTaskTypesEndpoint))
            .WithDescription("Get task types")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization()
            .Produces<IReadOnlyList<GetTaskTypesResponse>>();
    }

    public async Task<Ok<IReadOnlyList<GetTaskTypesResponse>>> HandleAsync(
        ISender sender,
        [AsParameters] GetTaskTypesQuery query,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(query, cancellationToken);
        return TypedResults.Ok(result);
    }
}
