using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace SASS.Chat.Features.Conversations.CreateConversation;

public sealed class CreateConversationEndpoint : IEndpoint<Ok<IdResult>, CreateConversationCommand, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("conversations", HandleAsync)
            .WithTags(nameof(Conversation))
            .WithName(nameof(CreateConversationEndpoint))
            .WithDescription("Create conversation")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization()
            .Produces<IdResult>(StatusCodes.Status201Created);
    }

    public async Task<Ok<IdResult>> HandleAsync(CreateConversationCommand command, ISender sender, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(command, cancellationToken);

        return TypedResults.Ok(result);
    }
}
