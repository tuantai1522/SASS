namespace SASS.Chat.Domain.AggregatesModel.Conversations;

public sealed class Message : Entity
{
    private Message()
    {
    }

    public static Message Create(Guid conversationId, string content, MessageRole role, long createdAt)
    {
        return new Message
        {
            ConversationId = conversationId,
            Content = content,
            Role = role,
            CreatedAt = createdAt
        };
    }

    public string Content { get; private set; } = null!;
    public MessageRole Role { get; private set; }
    public long CreatedAt { get; private set; }

    public Guid ConversationId { get; private set; }
    public Conversation Conversation { get; private set; } = null!;
}
