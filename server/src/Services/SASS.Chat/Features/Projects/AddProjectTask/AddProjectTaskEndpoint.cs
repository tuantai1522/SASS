using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace SASS.Chat.Features.Projects.AddProjectTask;

public sealed class AddProjectTaskEndpoint : IEndpoint<Ok<IdResult>, Guid, AddProjectTaskCommand, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("projects/{projectId:guid}/tasks", HandleAsync)
            .WithTags(nameof(Project))
            .WithName(nameof(AddProjectTaskEndpoint))
            .WithDescription("Add project task")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization()
            .Produces<IdResult>();
    }

    public async Task<Ok<IdResult>> HandleAsync(
        [FromRoute] Guid projectId,
        [FromBody] AddProjectTaskCommand command,
        ISender sender,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(command with { ProjectId = projectId }, cancellationToken);
        return TypedResults.Ok(result);
    }
}
