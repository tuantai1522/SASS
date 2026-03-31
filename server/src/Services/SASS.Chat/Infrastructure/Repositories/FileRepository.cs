using File = SASS.Chat.Domain.AggregatesModel.Files.File;
using SASS.Chat.Domain.AggregatesModel.Files;
using Microsoft.EntityFrameworkCore;

namespace SASS.Chat.Infrastructure.Repositories;

public sealed class FileRepository(ChatDbContext context) : IFileRepository
{
    private readonly ChatDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public IUnitOfWork UnitOfWork => _context;
    
    public async Task AddRangeAsync(IReadOnlyList<File> files, CancellationToken cancellationToken = default)
    {
        await _context.Files.AddRangeAsync(files, cancellationToken);
    }

    public Task<File?> GetByConversationAndUserAsync(
        Guid conversationId,
        Guid userId,
        Guid fileId,
        CancellationToken cancellationToken = default
    )
    {
        return _context.Files
            .AsNoTracking()
            .Where(x => x.Id == fileId)
            .Where(x => x.UserId == userId)
            .Where(x => x.ConversationFiles.Any(cf => cf.ConversationId == conversationId))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<File>> GetByIdsAsync(
        IReadOnlyList<Guid> fileIds,
        CancellationToken cancellationToken = default
    )
    {
        return await _context.Files
            .Where(x => fileIds.Contains(x.Id))
            .ToListAsync(cancellationToken);
    }
}
