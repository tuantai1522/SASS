namespace SASS.Chat.Configurations;

public sealed class QdrantOptions
{
    public const string SectionName = "Qdrant";

    public string Host { get; init; } = null!;
    public int Port { get; init; } = 6334;
    public bool Https { get; init; }
    public string? ApiKey { get; init; }
}
