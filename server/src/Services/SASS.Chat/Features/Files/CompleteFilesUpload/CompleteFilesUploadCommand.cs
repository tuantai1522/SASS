using MediatR;

namespace SASS.Chat.Features.Files.CompleteFilesUpload;

public sealed record CompleteFilesUploadCommand(Guid ConversationId, IReadOnlyList<Guid> FileIds)
    : IRequest<CompleteFilesUploadResponse>;
