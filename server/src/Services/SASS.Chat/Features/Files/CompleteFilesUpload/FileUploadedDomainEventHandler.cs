using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SASS.Chat.Domain.AggregatesModel.Files;
using SASS.Chat.Domain.Events.Files;
using SASS.Chat.Features.Files.FileEmbedding;
using SASS.Chat.Features.Files.FileStatusUpdated;
using SASS.Chat.Infrastructure;
using SASS.Chat.Realtime;
using Wolverine;
using Task = System.Threading.Tasks.Task;

namespace SASS.Chat.Features.Files.CompleteFilesUpload;

internal sealed class FileUploadedDomainEventHandler(
    ChatDbContext dbContext,
    IHubContext<ApplicationNotifier, IApplicationNotifier> applicationNotifier,
    IMessageBus messageBus) : INotificationHandler<FileUploadedDomainEvent>
{
    public async Task Handle(FileUploadedDomainEvent notification, CancellationToken cancellationToken)
    {
        var file = await dbContext.Files
            .FirstOrDefaultAsync(x => x.Id == notification.FileId, cancellationToken);

        if (file is null || file.UploadStatus != UploadStatus.Uploaded)
        {
            return;
        }

        file.MarkProcessing();
        
        // 1. To save changes in database
        await dbContext.SaveChangesAsync(cancellationToken);

        // 2. Publish notification event for current user to update file status
        await applicationNotifier.Clients.User(file.UserId.ToString()).FileStatusUpdated(new FileStatusUpdatedEventResponse(file.Id, file.UploadStatus));
        
        // 3. Publish notification event for embedding handler
        await messageBus.PublishAsync(new FileEmbeddingRequested(file.Id));
    }
}
