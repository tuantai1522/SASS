using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SASS.Chat.Domain.Events.Conversations;
using SASS.Chat.Infrastructure;
using SASS.Chat.Realtime;
using Task = System.Threading.Tasks.Task;

namespace SASS.Chat.Features.Conversations.CreateMessage;

internal sealed class MessageCreatedDomainEventHandler(
    ChatDbContext dbContext,
    IHubContext<ApplicationNotifier, IApplicationNotifier> applicationNotifier) : INotificationHandler<MessageCreatedDomainEvent>
{
    public async Task Handle(MessageCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var response = await dbContext.Messages
            .AsNoTracking()
            .Where(x => x.Id == notification.MessageId)
            .Select(x => new MessageCreatedEventResponse(x.ConversationId, x.Id, x.Content, x.CreatedAt, 
                x.SenderId, x.Sender != null ? x.Sender.DisplayName : null, x.Sender != null ? x.Sender.AvatarUrl : null) )
            .FirstOrDefaultAsync(cancellationToken);

        if (response != null)
        {
            await applicationNotifier.Clients.Group(Utils.Constants.Stream.Conversation(response.ConversationId)).MessageCreated(response);
        }
    }
}
