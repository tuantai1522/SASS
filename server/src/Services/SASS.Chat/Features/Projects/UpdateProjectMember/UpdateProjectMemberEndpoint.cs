using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace SASS.Chat.Features.Projects.UpdateProjectMember;

public sealed class UpdateProjectMemberEndpoint : IEndpoint<Ok<IdResult>, Guid, UpdateProjectMemberCommand, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("projects/{projectId:guid}/members", HandleAsync)
            .WithTags(nameof(Project))
            .WithName(nameof(UpdateProjectMemberEndpoint))
            .WithDescription("Update project member")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization()
            .Produces<IdResult>();
    }

    public async Task<Ok<IdResult>> HandleAsync(
        [FromRoute] Guid projectId,
        [FromBody] UpdateProjectMemberCommand command,
        ISender sender,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(command with { ProjectId = projectId }, cancellationToken);
        return TypedResults.Ok(result);
    }
}
