using Microsoft.EntityFrameworkCore;

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

    public async Task<IReadOnlyList<Conversation>> GetConversationsByCursorAsync(
        Guid userId,
        long? createdAtCursor,
        Guid? idCursor,
        int limit,
        bool isAscending,
        CancellationToken cancellationToken = default
    )
    {
        var query = _context.Conversations
            .AsNoTracking()
            .Where(x => x.UserId == userId);

        if (createdAtCursor.HasValue && idCursor.HasValue)
        {
            query = isAscending ? 
                query.Where(x => EF.Functions.GreaterThan(ValueTuple.Create(x.CreatedAt, x.Id), ValueTuple.Create(createdAtCursor, idCursor))) : 
                query.Where(x => EF.Functions.LessThan(ValueTuple.Create(x.CreatedAt, x.Id), ValueTuple.Create(createdAtCursor, idCursor)));
        }

        _ = isAscending;

        return await query
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.Id)
            .Take(limit + 1)
            .ToListAsync(cancellationToken);
    }
}
