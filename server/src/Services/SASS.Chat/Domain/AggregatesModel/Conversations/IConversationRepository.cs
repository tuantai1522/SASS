namespace SASS.Chat.Domain.AggregatesModel.Conversations;

public interface IConversationRepository : IRepository<Conversation>
{
    Task<Conversation> AddAsync(Conversation conversation, CancellationToken cancellationToken = default);
}
