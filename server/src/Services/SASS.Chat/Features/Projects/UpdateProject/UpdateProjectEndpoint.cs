using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace SASS.Chat.Features.Projects.UpdateProject;

public sealed class UpdateProjectEndpoint : IEndpoint<Ok<IdResult>, Guid, UpdateProjectCommand, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("projects/{projectId:guid}", HandleAsync)
            .WithTags(nameof(Project))
            .WithName(nameof(UpdateProjectEndpoint))
            .WithDescription("Update project")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization()
            .Produces<IdResult>();
    }

    public async Task<Ok<IdResult>> HandleAsync(
        [FromRoute] Guid projectId,
        [FromBody] UpdateProjectCommand command,
        ISender sender,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(command with { ProjectId = projectId }, cancellationToken);
        return TypedResults.Ok(result);
    }
}
