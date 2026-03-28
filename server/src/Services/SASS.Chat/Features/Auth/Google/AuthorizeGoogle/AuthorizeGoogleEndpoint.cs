using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace SASS.Chat.Features.Auth.Google.AuthorizeGoogle;

public sealed class AuthorizeGoogleEndpoint : IEndpoint<Ok<AuthorizeGoogleResponse>, ISender, string>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/google/authorize", HandleAsync)
            .WithTags(nameof(User))
            .WithName(nameof(AuthorizeGoogleEndpoint))
            .WithDescription("Authorize google account")
            .MapToApiVersion(ApiVersions.V1)
            .Produces<AuthorizeGoogleResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status502BadGateway)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }

    public async Task<Ok<AuthorizeGoogleResponse>> HandleAsync(ISender sender, [FromQuery] string code, CancellationToken cancellationToken = default)
    {
        var query = new AuthorizeGoogleQuery(code);
        var result = await sender.Send(query, cancellationToken);

        return TypedResults.Ok(result);
    }
}
