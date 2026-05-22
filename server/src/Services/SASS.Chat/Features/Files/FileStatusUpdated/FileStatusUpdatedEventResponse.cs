using SASS.Chat.Domain.AggregatesModel.Files;

namespace SASS.Chat.Features.Files.FileStatusUpdated;

public sealed record FileStatusUpdatedEventResponse(Guid FileId, UploadStatus UploadStatus);
