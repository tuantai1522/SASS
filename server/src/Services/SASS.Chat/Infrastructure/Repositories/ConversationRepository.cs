namespace SASS.Chat.Infrastructure.Repositories;

public sealed class ConversationRepository(ChatDbContext context) : IConversationRepository
{
    private readonly ChatDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public IUnitOfWork UnitOfWork => _context;

    public async Task<Conversation> AddAsync(Conversation conversation, CancellationToken cancellationToken = default)
    {
        var entry = await _context.Conversations.AddAsync(conversation, cancellationToken);
        return entry.Entity;
    }
}
