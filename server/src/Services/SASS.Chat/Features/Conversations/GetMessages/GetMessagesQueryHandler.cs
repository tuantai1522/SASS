using MediatR;
using Microsoft.EntityFrameworkCore;
using SASS.Chassis.Security.UserRetrieval;
using SASS.Chassis.Utilities.Guards;
using SASS.Chat.Infrastructure;

namespace SASS.Chat.Features.Conversations.GetMessages;

internal sealed class GetMessagesQueryHandler(
    ChatDbContext dbContext,
    IUserProvider userProvider) : IRequestHandler<GetMessagesQuery, CursorPagedResult<GetMessagesResponse>>
{
    public async Task<CursorPagedResult<GetMessagesResponse>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
    {
        Guid? lastId = null;
        long? createdAt = null;
        
        if (!string.IsNullOrWhiteSpace(request.CursorRequest.Cursor))
        {
            var decodedCursor = CursorToken.Decode(request.CursorRequest.Cursor);

            lastId = decodedCursor?.Id;
            createdAt = decodedCursor?.CreatedAt;
        }
        
        var conversationExists = await dbContext.Conversations
            .AsNoTracking()
            .AnyAsync(x => x.ConversationMembers.Any(cm => cm.MemberId == userProvider.UserId && 
                                                           !cm.IsDeleted), cancellationToken);

        Guard.Against.NotFound<Project>(conversationExists, request.ConversationId);
        
        var query = dbContext.Messages
            .AsNoTracking()
            .Where(x => x.ConversationId == request.ConversationId);

        if (createdAt.HasValue && lastId.HasValue)
        {
            query = request.CursorRequest.Order == Order.Asc
                ? query.Where(x => EF.Functions.GreaterThan(ValueTuple.Create(x.CreatedAt, x.Id), ValueTuple.Create(createdAt, lastId)))
                : query.Where(x => EF.Functions.LessThan(ValueTuple.Create(x.CreatedAt, x.Id), ValueTuple.Create(createdAt, lastId)));
        }

        var messages = await query
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.Id)
            .Select(x => new GetMessagesResponse(
                x.Id, x.Content, x.CreatedAt, x.SenderId == userProvider.UserId, 
                x.SenderId, x.Sender != null ? x.Sender.DisplayName : null, x.Sender != null ? x.Sender.AvatarUrl : null))
            .Take(request.CursorRequest.Limit + 1)
            .ToListAsync(cancellationToken);

        var hasNextPage = messages.Count > request.CursorRequest.Limit;
        var pageItems = hasNextPage
            ? messages.Take(request.CursorRequest.Limit).ToList()
            : messages.ToList();

        string? nextCursor = null;
        if (hasNextPage)
        {
            var last = pageItems[^1];
            nextCursor = CursorToken.Encode(new CursorToken(last.CreatedAt, last.Id));
        }

        return new CursorPagedResult<GetMessagesResponse>(pageItems, nextCursor, hasNextPage);
    }
}
