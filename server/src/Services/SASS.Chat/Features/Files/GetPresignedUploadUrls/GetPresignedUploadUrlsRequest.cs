namespace SASS.Chat.Features.Files.GetPresignedUploadUrls;

public sealed class GetPresignedUploadUrlsRequest
{
    public IReadOnlyList<PresignedUploadFileRequest> Files { get; init; } = [];
}

public sealed record PresignedUploadFileRequest(string FileName, string ContentType);
