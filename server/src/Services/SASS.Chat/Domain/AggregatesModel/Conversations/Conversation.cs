using SASS.Chat.Domain.Exceptions;

namespace SASS.Chat.Domain.AggregatesModel.Conversations;

public sealed class Conversation : Entity, IAggregateRoot
{
    private readonly List<ConversationFile> _conversationFiles = [];
    private readonly List<Message> _messages = [];

    private Conversation()
    {
    }

    public static Conversation Create(Guid userId, string name)
    {
        EnsureIdentity(userId, nameof(userId));
        EnsureRequiredText(name, nameof(name), 512);

        return new Conversation
        {
            UserId = userId,
            Name = name,
            LastMessageUpdatedAt = DateTimeOffset.Now.ToUnixTimeSeconds()
        };
    }

    public string Name { get; private set; } = null!;
    public long CreatedAt { get; init; } = DateTimeOffset.Now.ToUnixTimeSeconds();
    public long LastMessageUpdatedAt { get; private set; }

    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;

    public IReadOnlyCollection<ConversationFile> ConversationFiles => _conversationFiles;
    public IReadOnlyCollection<Message> Messages => _messages;

    public void UpdateLastMessageTimestamp(long timestamp)
    {
        EnsureUnixMilliseconds(timestamp, nameof(timestamp));

        if (timestamp < CreatedAt)
        {
            throw new ChatDomainException("timestamp must be greater than or equal to createdAt.");
        }

        if (timestamp < LastMessageUpdatedAt)
        {
            throw new ChatDomainException("timestamp cannot move backward.");
        }

        LastMessageUpdatedAt = timestamp;
    }

    public void AddConversationFile(ConversationFile conversationFile)
    {
        if (conversationFile.ConversationId != Id)
        {
            throw new ChatDomainException("Conversation file conversation id does not match current conversation.");
        }

        if (_conversationFiles.Any(x => x.ConversationId == conversationFile.ConversationId && x.FileId == conversationFile.FileId))
        {
            throw new ChatDomainException("Conversation file already exists in this conversation.");
        }

        _conversationFiles.Add(conversationFile);
    }

    public void AddMessage(Message message)
    {
        if (message.ConversationId != Id)
        {
            throw new ChatDomainException("Message conversation id does not match current conversation.");
        }

        _messages.Add(message);

        if (message.CreatedAt > LastMessageUpdatedAt)
        {
            LastMessageUpdatedAt = message.CreatedAt;
        }
    }

    private static void EnsureIdentity(Guid value, string fieldName)
    {
        if (value == Guid.Empty)
        {
            throw new ChatDomainException($"{fieldName} is invalid.");
        }
    }

    private static void EnsureRequiredText(string value, string fieldName, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ChatDomainException($"{fieldName} is required.");
        }

        if (value.Length > maxLength)
        {
            throw new ChatDomainException($"{fieldName} exceeds maximum length {maxLength}.");
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
