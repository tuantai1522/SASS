using MediatR;

namespace SASS.Chat.Features.Files.GetPresignedUploadUrls;

public sealed record GetPresignedUploadUrlsCommand(Guid ConversationId, GetPresignedUploadUrlsRequest Request)
    : IRequest<GetPresignedUploadUrlsResponse>;
