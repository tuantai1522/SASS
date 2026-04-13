using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace SASS.Chat.Features.Projects.CreateProject;

public sealed class CreateProjectEndpoint : IEndpoint<Ok<IdResult>, CreateProjectCommand, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("projects", HandleAsync)
            .WithTags(nameof(Project))
            .WithName(nameof(CreateProjectEndpoint))
            .WithDescription("Create project")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization()
            .Produces<IdResult>(StatusCodes.Status201Created);
    }

    public async Task<Ok<IdResult>> HandleAsync(
        CreateProjectCommand command,
        ISender sender,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(command, cancellationToken);
        return TypedResults.Ok(result);
    }
}
