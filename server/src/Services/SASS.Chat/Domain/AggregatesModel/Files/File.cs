using SASS.Chat.Domain.AggregatesModel.Conversations;
using SASS.Chat.Domain.AggregatesModel.Users;
using SASS.Chat.Domain.Exceptions;

namespace SASS.Chat.Domain.AggregatesModel.Files;

public sealed class File : Entity, IAggregateRoot
{
    private readonly List<ConversationFile> _conversationFiles = [];

    private File()
    {
    }

    public static File Create(Guid userId, string name, string key, UploadStatus uploadStatus, long createdAt)
    {
        EnsureIdentity(userId, nameof(userId));
        EnsureRequiredText(name, nameof(name), 512);
        EnsureRequiredText(key, nameof(key), 2048);
        EnsureEnum(uploadStatus, nameof(uploadStatus));
        EnsureUnixMilliseconds(createdAt, nameof(createdAt));

        return new File
        {
            UserId = userId,
            Name = name,
            Key = key,
            UploadStatus = uploadStatus,
            CreatedAt = createdAt
        };
    }

    public string Name { get; private set; } = null!;
    public string Key { get; private set; } = null!;
    public UploadStatus UploadStatus { get; private set; }
    public long CreatedAt { get; private set; }

    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;

    public IReadOnlyCollection<ConversationFile> ConversationFiles => _conversationFiles;

    public void UpdateUploadStatus(UploadStatus uploadStatus)
    {
        EnsureEnum(uploadStatus, nameof(uploadStatus));
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

    private static void EnsureIdentity(Guid value, string fieldName)
    {
        if (value == Guid.Empty)
        {
            throw new ChatDomainException($"{fieldName} is invalid.");
        }
    }

    private static void EnsureRequiredText(string value, string fieldName, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ChatDomainException($"{fieldName} is required.");
        }

        if (value.Length > maxLength)
        {
            throw new ChatDomainException($"{fieldName} exceeds maximum length {maxLength}.");
        }
    }

    private static void EnsureEnum(UploadStatus value, string fieldName)
    {
        if (!Enum.IsDefined(value))
        {
            throw new ChatDomainException($"{fieldName} is invalid.");
        }
    }

    private static void EnsureUnixMilliseconds(long value, string fieldName)
    {
        if (value <= 0)
        {
            throw new ChatDomainException($"{fieldName} must be unix milliseconds greater than zero.");
        }
    }
}
