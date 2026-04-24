using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using SASS.Chat.Features.Auth.SignIn;

namespace SASS.Chat.Features.Auth.RenewAccessToken;

public sealed class RenewAccessTokenEndpoint : IEndpoint<Results<Ok<RenewAccessTokenResponse>, UnauthorizedHttpResult>, RenewAccessTokenCommand, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/renew-token", HandleAsync)
            .WithTags(nameof(User))
            .WithName(nameof(RenewAccessTokenEndpoint))
            .WithDescription("Refresh token")
            .MapToApiVersion(ApiVersions.V1)
            .Produces<SignInResponse>()
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status404NotFound);
    }

    public async Task<Results<Ok<RenewAccessTokenResponse>, UnauthorizedHttpResult>> HandleAsync(
        [AsParameters] RenewAccessTokenCommand command,
        ISender sender,
        CancellationToken cancellationToken = default)
    {
        var response = await sender.Send(command, cancellationToken);
        return TypedResults.Ok(response);
    }
}
