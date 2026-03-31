namespace SASS.Chat.Features.Files.CompleteFilesUpload;

public sealed record CompleteFilesUploadResponse(IReadOnlyList<Guid> CompletedFileIds);
