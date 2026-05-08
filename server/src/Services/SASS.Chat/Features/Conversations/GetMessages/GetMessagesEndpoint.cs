using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace SASS.Chat.Features.Conversations.GetMessages;

public sealed class GetMessagesEndpoint : IEndpoint<Ok<CursorPagedResult<GetMessagesResponse>>, Guid, CursorPagedRequest, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("conversations/{conversationId:guid}/messages/query", HandleAsync)
            .WithTags(nameof(Conversation))
            .WithName(nameof(GetMessagesEndpoint))
            .WithDescription("Get messages with cursor pagination")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization()
            .Produces<CursorPagedResult<GetMessagesResponse>>();
    }

    public async Task<Ok<CursorPagedResult<GetMessagesResponse>>> HandleAsync(
        Guid conversationId,
        [FromBody] CursorPagedRequest request,
        ISender sender,
        CancellationToken cancellationToken = default)
    {
        var query = new GetMessagesQuery(conversationId, request);

        var result = await sender.Send(query, cancellationToken);

        return TypedResults.Ok(result);
    }
}
