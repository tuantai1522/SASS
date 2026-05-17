using MediatR;

namespace SASS.Chat.Features.Files.GetPresignedUploadUrls;

public sealed record GetPresignedUploadUrlsCommand(Guid ConversationId, IReadOnlyList<GetPresignedUploadFileItems> Files)
    : IRequest<GetPresignedUploadUrlsResponse>;
