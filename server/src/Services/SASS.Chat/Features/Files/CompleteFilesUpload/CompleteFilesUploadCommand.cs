using MediatR;

namespace SASS.Chat.Features.Files.CompleteFilesUpload;

public sealed record CompleteFilesUploadCommand(IReadOnlyList<Guid> FileIds)
    : IRequest<CompleteFilesUploadResponse>;
