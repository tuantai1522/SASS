using System.Text.Json.Serialization;
using Microsoft.Extensions.VectorData;

namespace SASS.Chassis.AI.Search;

public sealed class TextSnippet
{
    // 1536 is the default vector size for the OpenAI text-embedding-3-small model
    private const int VectorDimensions = 768;
    private const string VectorDistanceFunction = DistanceFunction.CosineSimilarity;
    public const string CollectionName = "data-sass-snippets";

    [VectorStoreKey(StorageName = "id")]
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    /// <summary>
    /// Key from entity (this can be ProductId, ProjectId, TaskId, ...)
    /// </summary>
    [VectorStoreData(StorageName = "user_id")]
    [JsonPropertyName("user_id")]
    public required Guid UserId { get; init; }

    [VectorStoreData(StorageName = "file_id")]
    [JsonPropertyName("file_id")]
    public required Guid FileId { get; init; }

    [VectorStoreData(IsFullTextIndexed = true, StorageName = "content")]
    [JsonPropertyName("content")]
    public required string Content { get; init; }

    [VectorStoreVector(VectorDimensions, DistanceFunction = VectorDistanceFunction, StorageName = "embedding")]
    [JsonPropertyName("embedding")]
    public ReadOnlyMemory<float> Vector { get; init; }

    /// <summary>
    /// From documents, chunk into multiple smaller documents, every document is an index
    /// </summary>
    [VectorStoreData(StorageName = "index")]
    [JsonPropertyName("index")]
    public int Index { get; set; }

    /// <summary>
    /// To store current page number if it has (for PDF, Word)
    /// </summary>
    [VectorStoreData(StorageName = "page_number")]
    [JsonPropertyName("page_number")]
    public int? PageNumber { get; set; }

    /// <summary>
    /// Index on that page if it has page number (for PDF, Word)
    /// </summary>
    [VectorStoreData(StorageName = "index_on_page")]
    [JsonPropertyName("index_on_page")]
    public int? IndexOnPage { get; set; }
}
