using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace SASS.Chat.Features.Projects.GetProjectById;

public sealed class GetProjectByIdEndpoint : IEndpoint<Ok<GetProjectByIdResponse>, Guid, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("projects/{projectId:guid}", HandleAsync)
            .WithTags(nameof(Project))
            .WithName(nameof(GetProjectByIdEndpoint))
            .WithDescription("Get project by id")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization()
            .Produces<GetProjectByIdResponse>();
    }

    public async Task<Ok<GetProjectByIdResponse>> HandleAsync(
        [FromRoute] Guid projectId,
        ISender sender,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetProjectByIdQuery(projectId), cancellationToken);
        return TypedResults.Ok(result);
    }
}
