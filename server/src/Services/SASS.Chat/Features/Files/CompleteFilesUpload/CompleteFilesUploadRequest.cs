namespace SASS.Chat.Features.Files.CompleteFilesUpload;

public sealed class CompleteFilesUploadRequest
{
    public IReadOnlyList<Guid> FileIds { get; init; } = [];
}
