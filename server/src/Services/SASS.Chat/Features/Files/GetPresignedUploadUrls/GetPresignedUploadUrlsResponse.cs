namespace SASS.Chat.Features.Files.GetPresignedUploadUrls;

public sealed record GetPresignedUploadUrlsResponse(IReadOnlyList<PresignedUploadFileItem> Files);

public sealed record PresignedUploadFileItem(
    Guid FileId,
    string FileName,
    string Key,
    string UploadUrl,
    string ContentType
);
