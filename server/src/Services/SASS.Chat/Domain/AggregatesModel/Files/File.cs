using SASS.Chat.Domain.Exceptions;

namespace SASS.Chat.Domain.AggregatesModel.Files;

public sealed class File : Entity, IAggregateRoot
{
    private readonly List<ConversationFile> _conversationFiles = [];

    private File()
    {
    }

    public static File Create(Guid userId, Guid conversationId, string name, string key, UploadStatus uploadStatus)
    {
        var file = new File
        {
            UserId = userId,
            Name = name,
            Key = key,
            UploadStatus = uploadStatus,
        };

        file.AddConversationFile(ConversationFile.Create(conversationId, file.Id));

        return file;
    }

    public string Name { get; private set; } = null!;
    public string Key { get; private set; } = null!;
    public UploadStatus UploadStatus { get; private set; }
    public long CreatedAt { get; init; } = DateTimeOffset.Now.ToUnixTimeSeconds();

    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;

    public IReadOnlyCollection<ConversationFile> ConversationFiles => _conversationFiles;

    public void UpdateUploadStatus(UploadStatus uploadStatus)
    {
        UploadStatus = uploadStatus;
    }

    public void AddConversationFile(ConversationFile conversationFile)
    {
        if (conversationFile.FileId != Id)
        {
            throw new ChatDomainException("Conversation file id does not match current file.");
        }

        if (_conversationFiles.Any(x => x.ConversationId == conversationFile.ConversationId && x.FileId == conversationFile.FileId))
        {
            throw new ChatDomainException("Conversation file already exists for this file.");
        }

        _conversationFiles.Add(conversationFile);
    }
}
