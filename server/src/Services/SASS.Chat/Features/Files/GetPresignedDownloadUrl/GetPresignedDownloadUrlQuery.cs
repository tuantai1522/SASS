using MediatR;

namespace SASS.Chat.Features.Files.GetPresignedDownloadUrl;

public sealed record GetPresignedDownloadUrlQuery(Guid ConversationId, Guid FileId)
    : IRequest<GetPresignedDownloadUrlResponse>;
