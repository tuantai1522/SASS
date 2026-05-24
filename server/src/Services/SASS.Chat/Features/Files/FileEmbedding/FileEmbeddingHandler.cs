using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SASS.Chassis.AI.ContentDecoders;
using SASS.Chassis.AI.Ingestion;
using SASS.Chassis.Storage;
using SASS.Chat.Domain.AggregatesModel.Files;
using SASS.Chat.Infrastructure;
using SASS.Chat.Infrastructure.Ingestion;
using Task = System.Threading.Tasks.Task;

namespace SASS.Chat.Features.Files.FileEmbedding;

public sealed class FileEmbeddingHandler(
    ChatDbContext dbContext,
    IOptions<MediaStorageOptions> mediaStorageOptions,
    IServiceProvider serviceProvider,
    IIngestionSource<FileDataIngestion> ingestionSource,
    ILogger<FileEmbeddingHandler> logger)
{
    public async Task Handle(FileEmbeddingRequested message, CancellationToken cancellationToken)
    {
        var file = await dbContext.Files
            .FirstOrDefaultAsync(x => x.Id == message.FileId, cancellationToken);

        if (file is null)
        {
            logger.LogWarning("Skipping file embedding because file {FileId} was not found.", message.FileId);
            return;
        }

        if (file.UploadStatus != UploadStatus.Processing)
        {
            logger.LogInformation(
                "Skipping file embedding for file {FileId} because status is {UploadStatus}.",
                file.Id,
                file.UploadStatus);
            return;
        }

        if (string.IsNullOrWhiteSpace(file.ContentType))
        {
            file.MarkFailed("File content type is missing.");
            await dbContext.SaveChangesAsync(cancellationToken);
            return;
        }

        var mediaStorage = serviceProvider.GetRequiredKeyedService<IMediaStorage>(mediaStorageOptions.Value.Provider);
        var contentDecoder = serviceProvider.GetKeyedService<IContentDecoder>(file.ContentType);

        if (contentDecoder is null)
        {
            file.MarkFailed($"File content type '{file.ContentType}' is not supported for embedding.");
            await dbContext.SaveChangesAsync(cancellationToken);
            return;
        }

        try
        {
            await using var stream = await mediaStorage.OpenReadAsync(file.Key, cancellationToken);
            var chunks = await contentDecoder.DecodeAsync(stream, file.ContentType, cancellationToken);

            var index = 0;
            foreach (var chunk in chunks)
            {
                await ingestionSource.IngestDataAsync(
                    new FileDataIngestion(
                        file.UserId.ToString(),
                        file.Id.ToString(),
                        chunk.Content,
                        index,
                        chunk.PageNumber,
                        chunk.IndexOnPage),
                    cancellationToken);

                index++;
            }

            file.MarkSuccess();
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to embed file {FileId}.", file.Id);
            file.MarkFailed(ex.Message);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
