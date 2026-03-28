namespace SASS.Chat.Domain.AggregatesModel.Conversations;

public interface IConversationRepository : IRepository<Conversation>
{
    Task<Conversation> AddAsync(Conversation conversation, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Conversation>> GetConversationsByCursorAsync(
        Guid userId,
        long? createdAtCursor,
        Guid? idCursor,
        int limit,
        bool isAscending,
        CancellationToken cancellationToken = default
    );
}
