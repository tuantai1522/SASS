using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace SASS.Chat.Features.Projects.GetProjects;

public sealed class GetProjectsEndpoint : IEndpoint<Ok<PagedResult<GetProjectsItemResponse>>, ISender, GetProjectsQuery>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("projects", HandleAsync)
            .WithTags(nameof(Project))
            .WithName(nameof(GetProjectsEndpoint))
            .WithDescription("Get projects with normal pagination")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization()
            .Produces<PagedResult<GetProjectsItemResponse>>();
    }

    public async Task<Ok<PagedResult<GetProjectsItemResponse>>> HandleAsync(
        ISender sender,
        [AsParameters] GetProjectsQuery query,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(query, cancellationToken);
        return TypedResults.Ok(result);
    }
}
