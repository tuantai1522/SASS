using MediatR;
using Microsoft.EntityFrameworkCore;
using SASS.Chat.Domain.AggregatesModel.Files;
using SASS.Chat.Domain.Events.Files;
using SASS.Chat.Features.Files.FileEmbedding;
using SASS.Chat.Infrastructure;
using Wolverine;
using Task = System.Threading.Tasks.Task;

namespace SASS.Chat.Features.Files.CompleteFilesUpload;

internal sealed class FileUploadedDomainEventHandler(
    ChatDbContext dbContext,
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
        await dbContext.SaveChangesAsync(cancellationToken);

        await messageBus.PublishAsync(new FileEmbeddingRequested(file.Id));
    }
}
