using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace SASS.Chat.Features.Auth.SignIn;

public sealed class SignInEndpoint : IEndpoint<Results<Ok<SignInResponse>, UnauthorizedHttpResult>, SignInCommand, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/sign-in", HandleAsync)
            .WithTags(nameof(User))
            .WithName(nameof(SignInEndpoint))
            .WithDescription("Sign in using email and password")
            .MapToApiVersion(ApiVersions.V1)
            .Produces<SignInResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }

    public async Task<Results<Ok<SignInResponse>, UnauthorizedHttpResult>> HandleAsync(
        SignInCommand command,
        ISender sender,
        CancellationToken cancellationToken = default)
    {
        var response = await sender.Send(command, cancellationToken);
        return TypedResults.Ok(response);
    }
}
