using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace SASS.Chat.Features.Users.GetMe;

public sealed class GetMeEndpoint : IEndpoint<Ok<GetMeResponse>, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/me", HandleAsync)
            .WithTags(nameof(User))
            .WithName(nameof(GetMeEndpoint))
            .WithDescription("Get me information")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization()
            .Produces<GetMeResponse>();
    }

    public async Task<Ok<GetMeResponse>> HandleAsync(
        ISender sender,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetMeQuery(), cancellationToken);
        return TypedResults.Ok(result);
    }
}
