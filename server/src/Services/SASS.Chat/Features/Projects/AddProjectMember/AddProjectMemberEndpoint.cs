using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace SASS.Chat.Features.Projects.AddProjectMember;

public sealed class AddProjectMemberEndpoint : IEndpoint<Ok<IdResult>, Guid, AddProjectMemberCommand, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("projects/{projectId:guid}/members", HandleAsync)
            .WithTags(nameof(Project))
            .WithName(nameof(AddProjectMemberEndpoint))
            .WithDescription("Add project member")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization()
            .Produces<IdResult>();
    }

    public async Task<Ok<IdResult>> HandleAsync(
        [FromRoute] Guid projectId,
        [FromBody] AddProjectMemberCommand command,
        ISender sender,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(command with { ProjectId = projectId }, cancellationToken);
        return TypedResults.Ok(result);
    }
}
