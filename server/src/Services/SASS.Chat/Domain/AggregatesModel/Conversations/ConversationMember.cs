namespace SASS.Chat.Domain.AggregatesModel.Conversations;

public sealed class ConversationMember : ISoftDelete
{
    public Guid ConversationId { get; private init; }

    public Guid MemberId { get; private init; }

    public bool IsOwner { get; private set; }
    public long JoinedAt { get; private init; }

    public bool IsDeleted { get; set; }
    private ConversationMember()
    {
    }

    public static ConversationMember Create(Guid conversationId, Guid memberId, bool isOwner)
    {
        return new ConversationMember
        {
            ConversationId = conversationId,
            MemberId = memberId,
            IsOwner = isOwner,
            JoinedAt = DateTimeOffset.Now.ToUnixTimeSeconds()
        };
    }
    
    public void Delete()
    {
        IsDeleted = true;
    }

}
