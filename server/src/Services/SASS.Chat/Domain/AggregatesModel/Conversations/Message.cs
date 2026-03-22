using SASS.Chat.Domain.Exceptions;

namespace SASS.Chat.Domain.AggregatesModel.Conversations;

public sealed class Message : Entity
{
    private Message()
    {
    }

    public static Message Create(Guid conversationId, string content, MessageRole role, long createdAt)
    {
        EnsureIdentity(conversationId, nameof(conversationId));
        EnsureRequiredText(content, nameof(content));
        EnsureEnum(role, nameof(role));
        EnsureUnixMilliseconds(createdAt, nameof(createdAt));

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

    private static void EnsureIdentity(Guid value, string fieldName)
    {
        if (value == Guid.Empty)
        {
            throw new ChatDomainException($"{fieldName} is invalid.");
        }
    }

    private static void EnsureRequiredText(string value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ChatDomainException($"{fieldName} is required.");
        }
    }

    private static void EnsureEnum(MessageRole value, string fieldName)
    {
        if (!Enum.IsDefined(value))
        {
            throw new ChatDomainException($"{fieldName} is invalid.");
        }
    }

    private static void EnsureUnixMilliseconds(long value, string fieldName)
    {
        if (value <= 0)
        {
            throw new ChatDomainException($"{fieldName} must be unix milliseconds greater than zero.");
        }
    }
}
