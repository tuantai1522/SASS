namespace SASS.Chat.Domain.AggregatesModel.Files;

public interface IFileRepository : IRepository<File>
{
    Task AddRangeAsync(IReadOnlyList<File> files, CancellationToken cancellationToken = default);

    Task<File?> GetByConversationAndUserAsync(
        Guid conversationId,
        Guid userId,
        Guid fileId,
        CancellationToken cancellationToken = default
    );

    Task<IReadOnlyList<File>> GetByIdsAsync(
        IReadOnlyList<Guid> fileIds,
        CancellationToken cancellationToken = default
    );
}
