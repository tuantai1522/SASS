using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SASS.Chassis.Security.UserRetrieval;
using SASS.Chassis.Storage;
using SASS.Chassis.Utilities.Guards;
using SASS.Chat.Infrastructure;

namespace SASS.Chat.Features.Files.GetPresignedDownloadUrl;

internal sealed class GetPresignedDownloadUrlQueryHandler(
    ChatDbContext dbContext,
    IUserProvider userProvider,
    IOptions<MediaStorageOptions> mediaStorageOptions,
    IServiceProvider serviceProvider
) : IRequestHandler<GetPresignedDownloadUrlQuery, GetPresignedDownloadUrlResponse>
{
    public async Task<GetPresignedDownloadUrlResponse> Handle(GetPresignedDownloadUrlQuery request, CancellationToken cancellationToken)
    {
        var file = await dbContext.Files
            .AsNoTracking()
            .Where(x => x.Id == request.FileId)
            .Where(x => x.UserId == userProvider.UserId)
            .Where(x => x.ConversationFiles.Any(cf => cf.ConversationId == request.ConversationId))
            .Select(x => new { x.Id, x.Name, x.Key })
            .FirstOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(file, request.ConversationId);

        var mediaStorage = serviceProvider.GetRequiredKeyedService<IMediaStorage>(mediaStorageOptions.Value.Provider);
        var downloadUrl = await mediaStorage.GetPresignedUrl(file.Key);

        return new GetPresignedDownloadUrlResponse(file.Id, file.Name, downloadUrl);
    }
}
