using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace SASS.Chat.Features.Projects.UpdateProjectTask;

public sealed class UpdateProjectTaskEndpoint : IEndpoint<Ok<IdResult>, Guid, UpdateProjectTaskCommand, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("projects/{projectId:guid}/tasks", HandleAsync)
            .WithTags(nameof(Project))
            .WithName(nameof(UpdateProjectTaskEndpoint))
            .WithDescription("Update project task")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization()
            .Produces<IdResult>();
    }

    public async Task<Ok<IdResult>> HandleAsync(
        [FromRoute] Guid projectId,
        [FromBody] UpdateProjectTaskCommand command,
        ISender sender,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(command with { ProjectId = projectId }, cancellationToken);
        return TypedResults.Ok(result);
    }
}
