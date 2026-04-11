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

    public void Rename(string name)
    {
        Name = name;
    }

    public void UpdateLastMessageTimestamp(long timestamp)
    {
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
}
