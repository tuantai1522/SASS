using SASS.Chat.Domain.Exceptions;

namespace SASS.Chat.Domain.AggregatesModel.Conversations;

public sealed class Conversation : Entity, IAggregateRoot
{
    private readonly List<Message> _messages = [];
    private readonly List<ConversationMember> _conversationMembers = [];

    private Conversation()
    {
    }

    public static Conversation Create(Guid userId, string name)
    {
        var conversation = new Conversation
        {
            UserId = userId,
            Name = name,
            LastMessageUpdatedAt = DateTimeOffset.Now.ToUnixTimeSeconds()
        };
        
        conversation.AddConversationMember(ConversationMember.Create(conversation.Id, userId, true));

        return conversation;
    }

    public string Name { get; private set; } = null!;
    public long CreatedAt { get; init; } = DateTimeOffset.Now.ToUnixTimeSeconds();
    public long LastMessageUpdatedAt { get; private set; }

    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;

    public IReadOnlyCollection<Message> Messages => _messages;
    public IReadOnlyCollection<ConversationMember> ConversationMembers => _conversationMembers;

    public void ChangeName(string name)
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

    public void AddConversationMember(ConversationMember conversationMember)
    {
        if (_conversationMembers.Any(x => x.ConversationId == conversationMember.ConversationId && x.MemberId == conversationMember.MemberId))
        {
            throw new ChatDomainException("Conversation member already exists in this conversation.");
        }

        _conversationMembers.Add(conversationMember);
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
