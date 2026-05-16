using Microsoft.Extensions.AI;
using Microsoft.Extensions.VectorData;
using SASS.Chassis.AI.Ingestion;
using SASS.Chassis.AI.Search;
using Task = System.Threading.Tasks.Task;

namespace SASS.Chat.Infrastructure.Ingestion;

internal sealed class FileDataIngestor(
    IEmbeddingGenerator<string, Embedding<float>> embeddingGenerator,
    VectorStoreCollection<Guid, TextSnippet> vectorCollection) : IIngestionSource<FileDataIngestion>
{
    public async Task IngestDataAsync(FileDataIngestion data, CancellationToken cancellationToken = default)
    {
        await vectorCollection.EnsureCollectionExistsAsync(cancellationToken);

        var embeddings = await embeddingGenerator.GenerateVectorAsync(
            data.Content,
            cancellationToken: cancellationToken
        );

        var record = new TextSnippet
        {
            Id = Guid.NewGuid(),
            UserId = data.UserId,
            Content = data.Content,
            Vector = embeddings,
            Index = data.Index,
            IndexOnPage = data.IndexOnPage,
            PageNumber = data.PageNumber
        };

        await vectorCollection.UpsertAsync(record, cancellationToken);
    }
}
