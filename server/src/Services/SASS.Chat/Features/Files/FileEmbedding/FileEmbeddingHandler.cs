using Task = System.Threading.Tasks.Task;

namespace SASS.Chat.Features.Files.FileEmbedding;

public sealed class FileEmbeddingHandler
{
    public Task Handle(FileEmbeddingRequested message)
    {
        // Todo: To add service handle embedding files
        Console.WriteLine($"[FileEmbeddingRequested] Processing file {message.FileId}");
        return Task.CompletedTask;
    }
}
