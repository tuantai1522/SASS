using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using SASS.Chat.Features.Auth.SignIn;

namespace SASS.Chat.Features.Auth.SignOut;

public sealed class SignOutEndpoint : IEndpoint<Results<Ok<Unit>, UnauthorizedHttpResult>, SignOutCommand, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/sign-out", HandleAsync)
            .WithTags(nameof(User))
            .WithName(nameof(SignOutEndpoint))
            .WithDescription("Sign out user by deleting refresh token cookie")
            .MapToApiVersion(ApiVersions.V1)
            .Produces<SignInResponse>()
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }

    public async Task<Results<Ok<Unit>, UnauthorizedHttpResult>> HandleAsync(SignOutCommand command, ISender sender, CancellationToken cancellationToken = default)
    {
        var response = await sender.Send(command, cancellationToken);
        return TypedResults.Ok(response);
    }
}
