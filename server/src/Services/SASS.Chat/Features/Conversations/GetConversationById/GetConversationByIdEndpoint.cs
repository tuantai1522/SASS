using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace SASS.Chat.Features.Conversations.GetConversationById;

public sealed class GetConversationByIdEndpoint : IEndpoint<Ok<GetConversationByIdResponse>, Guid, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("conversations/{conversationId:guid}", HandleAsync)
            .WithTags(nameof(Conversation))
            .WithName(nameof(GetConversationByIdEndpoint))
            .WithDescription("Get conversation by id")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization()
            .Produces<GetConversationByIdResponse>();
    }

    public async Task<Ok<GetConversationByIdResponse>> HandleAsync(
        [FromRoute] Guid conversationId,
        ISender sender,
        CancellationToken cancellationToken = default
    )
    {
        var result = await sender.Send(new GetConversationByIdQuery(conversationId), cancellationToken);
        return TypedResults.Ok(result);
    }
}
