using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace SASS.Chat.Features.Projects.DeleteProjectTask;

public sealed class DeleteProjectTaskEndpoint : IEndpoint<Ok<IdResult>, Guid, Guid, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("projects/{projectId:guid}/tasks/{taskId:guid}", HandleAsync)
            .WithTags(nameof(Project))
            .WithName(nameof(DeleteProjectTaskEndpoint))
            .WithDescription("Update project task")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization()
            .Produces<IdResult>();
    }

    public async Task<Ok<IdResult>> HandleAsync(
        [FromRoute] Guid projectId,
        [FromRoute] Guid taskId,
        ISender sender,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new DeleteProjectTaskCommand(projectId, taskId), cancellationToken);
        return TypedResults.Ok(result);
    }
}
