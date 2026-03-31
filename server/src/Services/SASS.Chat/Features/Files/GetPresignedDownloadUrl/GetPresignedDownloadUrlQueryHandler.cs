using MediatR;
using Microsoft.Extensions.Options;
using SASS.Chassis.Security.UserRetrieval;
using SASS.Chassis.Storage;
using SASS.Chassis.Utilities.Guards;
using SASS.Chat.Domain.AggregatesModel.Files;
using File = SASS.Chat.Domain.AggregatesModel.Files.File;

namespace SASS.Chat.Features.Files.GetPresignedDownloadUrl;

internal sealed class GetPresignedDownloadUrlQueryHandler(
    IFileRepository fileRepository,
    IUserProvider userProvider,
    IOptions<MediaStorageOptions> mediaStorageOptions,
    IServiceProvider serviceProvider
) : IRequestHandler<GetPresignedDownloadUrlQuery, GetPresignedDownloadUrlResponse>
{
    public async Task<GetPresignedDownloadUrlResponse> Handle(GetPresignedDownloadUrlQuery request, CancellationToken cancellationToken)
    {
        var file = await fileRepository.GetByConversationAndUserAsync(
            request.ConversationId,
            userProvider.UserId,
            request.FileId,
            cancellationToken
        );

        Guard.Against.NotFound(file, request.ConversationId);

        var mediaStorage = serviceProvider.GetRequiredKeyedService<IMediaStorage>(mediaStorageOptions.Value.Provider);
        var downloadUrl = await mediaStorage.GetPresignedUrl(file.Key);

        return new GetPresignedDownloadUrlResponse(file.Id, file.Name, downloadUrl);
    }
}
