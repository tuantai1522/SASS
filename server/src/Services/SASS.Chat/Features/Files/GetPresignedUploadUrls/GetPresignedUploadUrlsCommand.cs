using MediatR;

namespace SASS.Chat.Features.Files.GetPresignedUploadUrls;

public sealed record GetPresignedUploadUrlsCommand(IReadOnlyList<GetPresignedUploadFileItems> Files)
    : IRequest<GetPresignedUploadUrlsResponse>;
