namespace SASS.Chat.Features.Files.GetPresignedDownloadUrl;

public sealed record GetPresignedDownloadUrlResponse(Guid FileId, string FileName, string DownloadUrl);
