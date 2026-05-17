using SASS.Chat.Domain.Exceptions;
using SASS.Chat.Domain.Events.Files;

namespace SASS.Chat.Domain.AggregatesModel.Files;

public sealed class File : Entity, IAggregateRoot
{
    private File()
    {
    }

    public static File Create(Guid userId, string name, string key, UploadStatus uploadStatus)
    {
        var file = new File
        {
            UserId = userId,
            Name = name,
            Key = key,
            UploadStatus = uploadStatus,
        };

        return file;
    }

    public string Name { get; private set; } = null!;
    public string Key { get; private set; } = null!;
    public UploadStatus UploadStatus { get; private set; }
    public long CreatedAt { get; init; } = DateTimeOffset.Now.ToUnixTimeSeconds();

    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;

    public void UpdateUploadStatus(UploadStatus uploadStatus)
    {
        UploadStatus = uploadStatus;
    }

    public void MarkUploaded()
    {
        UploadStatus = UploadStatus.Uploaded;
        RegisterDomainEvent(new FileUploadedDomainEvent(Id));
    }

    public void MarkProcessing()
    {
        UploadStatus = UploadStatus.Processing;
    }
}
