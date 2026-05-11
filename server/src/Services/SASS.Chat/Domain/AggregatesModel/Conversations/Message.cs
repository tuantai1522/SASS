using SASS.Chat.Domain.Events.Conversations;

namespace SASS.Chat.Domain.AggregatesModel.Conversations;

public sealed class Message : Entity
{
    private Message()
    {
    }

    public static Message Create(Guid conversationId, string content, Guid senderId)
    {
        var message = new Message
        {
            ConversationId = conversationId,
            Content = content,
            SenderId = senderId,
        };

        message.RegisterDomainEvent(new MessageCreatedDomainEvent(message.Id));

        return message;
    }

    public string Content { get; private set; } = null!;
    
    public Guid? SenderId { get; private set; }
    public User? Sender { get; private set; }

    public long CreatedAt { get; private init; } = DateTimeOffset.Now.ToUnixTimeSeconds();

    public Guid ConversationId { get; private set; }
    public Conversation Conversation { get; private set; } = null!;
}
