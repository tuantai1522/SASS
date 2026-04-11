using MediatR;
using Microsoft.Extensions.Options;
using SASS.Chassis.Security.UserRetrieval;
using SASS.Chassis.Storage;
using SASS.Chat.Domain.AggregatesModel.Files;
using SASS.Chat.Infrastructure;
using File = SASS.Chat.Domain.AggregatesModel.Files.File;

namespace SASS.Chat.Features.Files.GetPresignedUploadUrls;

internal sealed class GetPresignedUploadUrlsCommandHandler(
    ChatDbContext dbContext,
    IUserProvider userProvider,
    IOptions<MediaStorageOptions> mediaStorageOptions,
    IServiceProvider serviceProvider
) : IRequestHandler<GetPresignedUploadUrlsCommand, GetPresignedUploadUrlsResponse>
{
    public async Task<GetPresignedUploadUrlsResponse> Handle(GetPresignedUploadUrlsCommand request, CancellationToken cancellationToken)
    {
        var mediaStorage = serviceProvider.GetRequiredKeyedService<IMediaStorage>(mediaStorageOptions.Value.Provider);
        var destinationPath = $"conversations/{request.ConversationId}";

        var responseItems = new List<PresignedUploadFileItem>(request.Request.Files.Count);

        List<File> files = [];
        foreach (var input in request.Request.Files)
        {
            var contentType = string.IsNullOrWhiteSpace(input.ContentType)
                ? "application/octet-stream"
                : input.ContentType;

            var fileName = input.FileName;

            var (key, uploadUrl) = await mediaStorage.GetPresignedUrl(destinationPath, fileName, contentType);

            var file = File.Create(userProvider.UserId, request.ConversationId, fileName, key, UploadStatus.Pending);
            files.Add(file);

            responseItems.Add(new PresignedUploadFileItem(file.Id, file.Name, file.Key, uploadUrl, contentType));
        }

        await dbContext.Files.AddRangeAsync(files, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new GetPresignedUploadUrlsResponse(responseItems);
    }
}
