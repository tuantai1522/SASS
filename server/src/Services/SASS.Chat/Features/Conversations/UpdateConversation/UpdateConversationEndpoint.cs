using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace SASS.Chat.Features.Conversations.UpdateConversation;

public sealed class UpdateConversationEndpoint : IEndpoint<Ok<IdResult>, Guid, UpdateConversationRequest, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("conversations/{conversationId:guid}", HandleAsync)
            .WithTags(nameof(Conversation))
            .WithName(nameof(UpdateConversationEndpoint))
            .WithDescription("Update conversation name")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization()
            .Produces<IdResult>();
    }

    public async Task<Ok<IdResult>> HandleAsync(
        [FromRoute] Guid conversationId,
        [FromBody] UpdateConversationRequest request,
        ISender sender,
        CancellationToken cancellationToken = default
    )
    {
        var result = await sender.Send(new UpdateConversationCommand(conversationId, request.Name), cancellationToken);
        return TypedResults.Ok(result);
    }
}
