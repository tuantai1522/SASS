using MediatR;
using SASS.Chassis.Security.UserRetrieval;

namespace SASS.Chat.Features.Conversations.GetConversations;

internal sealed class GetConversationsQueryHandler(
    IConversationRepository conversationRepository,
    IUserProvider userProvider
) : IRequestHandler<GetConversationsQuery, CursorPagedResponse<GetConversationsResponse>>
{
    public async Task<CursorPagedResponse<GetConversationsResponse>> Handle(GetConversationsQuery request, CancellationToken cancellationToken)
    {
        Guid? lastId = null;
        long? createdAt = null;
        
        if (!string.IsNullOrWhiteSpace(request.CursorRequest.Cursor))
        {
            var decodedCursor = CursorToken.Decode(request.CursorRequest.Cursor);

            lastId = decodedCursor?.Id;
            createdAt = decodedCursor?.CreatedAt;
        }

        var conversations = await conversationRepository.GetConversationsByCursorAsync(
            userProvider.UserId,
            createdAt,
            lastId,
            request.CursorRequest.Limit,
            request.CursorRequest.IsAscending,
            cancellationToken
        );

        var hasNextPage = conversations.Count > request.CursorRequest.Limit;
        var pageItems = hasNextPage
            ? conversations.Take(request.CursorRequest.Limit).ToList()
            : conversations.ToList();

        string? nextCursor = null;
        if (hasNextPage)
        {
            var last = pageItems[^1];
            nextCursor = CursorToken.Encode(new CursorToken(last.CreatedAt, last.Id));
        }

        var items = pageItems
            .Select(x => new GetConversationsResponse(x.Id, x.Name, x.CreatedAt, x.LastMessageUpdatedAt))
            .ToList();

        return new CursorPagedResponse<GetConversationsResponse>(items, nextCursor, hasNextPage);
    }
}
