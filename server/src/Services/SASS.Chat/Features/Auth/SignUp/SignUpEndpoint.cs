using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace SASS.Chat.Features.Auth.SignUp;

public sealed class SignUpEndpoint : IEndpoint<Ok<IdResult>, SignUpCommand, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/sign-up", HandleAsync)
            .WithTags(nameof(User))
            .WithName(nameof(SignUpEndpoint))
            .WithDescription("Sign up using email and password")
            .MapToApiVersion(ApiVersions.V1)
            .Produces<IdResult>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status409Conflict)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }

    public async Task<Ok<IdResult>> HandleAsync(
        SignUpCommand command,
        ISender sender,
        CancellationToken cancellationToken = default)
    {
        var response = await sender.Send(command, cancellationToken);
        return TypedResults.Ok(response);
    }
}
