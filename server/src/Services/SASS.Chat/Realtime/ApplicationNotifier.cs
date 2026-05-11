using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SASS.Chat.Infrastructure;
using Task = System.Threading.Tasks.Task;

namespace SASS.Chat.Realtime;

[Authorize]
public sealed class ApplicationNotifier(ChatDbContext dbContext) : Hub<IApplicationNotifier>
{
    public override async Task OnConnectedAsync()
    {
        var userId = GetCurrentUserId();

        var conversationIds = await dbContext.Conversations
            .AsNoTracking()
            .Where(c => c.ConversationMembers.Any(cm =>
                cm.MemberId == userId &&
                !cm.IsDeleted))
            .Select(c => c.Id)
            .ToListAsync();

        foreach (var conversationId in conversationIds)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, Utils.Constants.Stream.Conversation(conversationId));
        }
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = GetCurrentUserId();

        var conversationIds = await dbContext.Conversations
            .AsNoTracking()
            .Where(c => c.ConversationMembers.Any(cm =>
                cm.MemberId == userId &&
                !cm.IsDeleted))
            .Select(c => c.Id)
            .ToListAsync();

        foreach (var conversationId in conversationIds)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, Utils.Constants.Stream.Conversation(conversationId));
        }
    }

    private Guid GetCurrentUserId()
    {
        var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        return Guid.TryParse(userId, out var parsedUserId)
            ? parsedUserId
            : throw new HubException("Unauthorized");
    }
}
