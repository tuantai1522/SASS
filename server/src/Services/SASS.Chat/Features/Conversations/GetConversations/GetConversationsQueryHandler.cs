using MediatR;
using Microsoft.EntityFrameworkCore;
using SASS.Chassis.Security.UserRetrieval;
using SASS.Chat.Infrastructure;

namespace SASS.Chat.Features.Conversations.GetConversations;

internal sealed class GetConversationsQueryHandler(
    ChatDbContext dbContext,
    IUserProvider userProvider
) : IRequestHandler<GetConversationsQuery, CursorPagedResult<GetConversationsResponse>>
{
    public async Task<CursorPagedResult<GetConversationsResponse>> Handle(GetConversationsQuery request, CancellationToken cancellationToken)
    {
        Guid? lastId = null;
        long? lastMessageUpdatedAt = null;
        
        if (!string.IsNullOrWhiteSpace(request.CursorRequest.Cursor))
        {
            var decodedCursor = CursorToken.Decode(request.CursorRequest.Cursor);

            lastId = decodedCursor?.Id;
            lastMessageUpdatedAt = decodedCursor?.CreatedAt;
        }

        var query = dbContext.Conversations
            .AsNoTracking()
            .Where(x => x.ConversationMembers.Any(cm => cm.MemberId == userProvider.UserId && 
                                                        !cm.IsDeleted));

        if (lastMessageUpdatedAt.HasValue && lastId.HasValue)
        {
            query = request.CursorRequest.Order == Order.Asc
                ? query.Where(x => EF.Functions.GreaterThan(ValueTuple.Create(x.LastMessageUpdatedAt, x.Id), ValueTuple.Create(lastMessageUpdatedAt, lastId)))
                : query.Where(x => EF.Functions.LessThan(ValueTuple.Create(x.LastMessageUpdatedAt, x.Id), ValueTuple.Create(lastMessageUpdatedAt, lastId)));
        }

        query = request.CursorRequest.Order == Order.Asc
            ? query.OrderBy(x => x.LastMessageUpdatedAt).ThenBy(x => x.Id)
            : query.OrderByDescending(x => x.LastMessageUpdatedAt).ThenByDescending(x => x.Id);
        
        var conversations = await query
            .Select(x => new GetConversationsResponse(x.Id, x.Name, x.CreatedAt, x.LastMessageUpdatedAt))
            .Take(request.CursorRequest.Limit + 1)
            .ToListAsync(cancellationToken);

        var hasNextPage = conversations.Count > request.CursorRequest.Limit;
        var pageItems = hasNextPage
            ? conversations.Take(request.CursorRequest.Limit).ToList()
            : conversations.ToList();

        string? nextCursor = null;
        if (hasNextPage)
        {
            var last = pageItems[^1];
            nextCursor = CursorToken.Encode(new CursorToken(last.LastMessageUpdatedAt, last.Id));
        }

        return new CursorPagedResult<GetConversationsResponse>(pageItems, nextCursor, hasNextPage);
    }
}
