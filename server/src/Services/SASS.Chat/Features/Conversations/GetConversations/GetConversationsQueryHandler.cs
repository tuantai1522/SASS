using MediatR;
using Microsoft.EntityFrameworkCore;
using SASS.Chassis.Security.UserRetrieval;
using SASS.Chat.Infrastructure;

namespace SASS.Chat.Features.Conversations.GetConversations;

internal sealed class GetConversationsQueryHandler(
    ChatDbContext dbContext,
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

        var query = dbContext.Conversations
            .AsNoTracking()
            .Where(x => x.UserId == userProvider.UserId);

        if (createdAt.HasValue && lastId.HasValue)
        {
            query = request.CursorRequest.IsAscending
                ? query.Where(x => EF.Functions.GreaterThan(ValueTuple.Create(x.CreatedAt, x.Id), ValueTuple.Create(createdAt, lastId)))
                : query.Where(x => EF.Functions.LessThan(ValueTuple.Create(x.CreatedAt, x.Id), ValueTuple.Create(createdAt, lastId)));
        }

        var conversations = await query
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.Id)
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
            nextCursor = CursorToken.Encode(new CursorToken(last.CreatedAt, last.Id));
        }

        return new CursorPagedResponse<GetConversationsResponse>(pageItems, nextCursor, hasNextPage);
    }
}
