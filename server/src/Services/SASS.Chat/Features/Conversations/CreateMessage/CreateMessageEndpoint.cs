using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace SASS.Chat.Features.Conversations.CreateMessage;

public sealed class CreateMessageEndpoint : IEndpoint<Ok<IdResult>, Guid, CreateMessageCommand, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("conversations/{conversationId:guid}/messages", HandleAsync)
            .WithTags(nameof(Conversation))
            .WithName(nameof(CreateMessageEndpoint))
            .WithDescription("Create message in conversation")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization()
            .Produces<IdResult>(StatusCodes.Status201Created);
    }

    public async Task<Ok<IdResult>> HandleAsync(
        [FromRoute] Guid conversationId,
        [FromBody] CreateMessageCommand command,
        ISender sender,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(command with { ConversationId = conversationId }, cancellationToken);

        return TypedResults.Ok(result);
    }
}
