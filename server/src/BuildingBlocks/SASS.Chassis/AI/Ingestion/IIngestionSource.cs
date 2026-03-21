namespace SASS.Chassis.AI.Ingestion;

/// <summary>
/// This will ingest data received from client after chunking
/// </summary>
public interface IIngestionSource<in T> where T : IIngestionData
{
    Task IngestDataAsync(T data, CancellationToken cancellationToken = default);
}
