using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace SASS.Chat.Features.Conversations.GetConversations;

public sealed class GetConversationsEndpoint : IEndpoint<Ok<CursorPagedResult<GetConversationsResponse>>, ISender, CursorPagedRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("conversations", HandleAsync)
            .WithTags(nameof(Conversation))
            .WithName(nameof(GetConversationsEndpoint))
            .WithDescription("Get user conversations with cursor pagination")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization()
            .Produces<CursorPagedResult<GetConversationsResponse>>();
    }

    public async Task<Ok<CursorPagedResult<GetConversationsResponse>>> HandleAsync(
        ISender sender,
        [AsParameters] CursorPagedRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var query = new GetConversationsQuery(request);
        var result = await sender.Send(query, cancellationToken);
        return TypedResults.Ok(result);
    }
}
