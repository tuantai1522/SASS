using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace SASS.Chat.Features.Projects.DeleteProject;

public sealed class DeleteProjectEndpoint : IEndpoint<Ok<IdResult>, Guid, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("projects/{projectId:guid}", HandleAsync)
            .WithTags(nameof(Project))
            .WithName(nameof(DeleteProjectEndpoint))
            .WithDescription("Delete project")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization()
            .Produces<IdResult>();
    }

    public async Task<Ok<IdResult>> HandleAsync(
        [FromRoute] Guid projectId,
        ISender sender,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new DeleteProjectCommand(projectId), cancellationToken);
        return TypedResults.Ok(result);
    }
}
