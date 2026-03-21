namespace SASS.Chassis.AI.Settings;

public sealed class OpenRouterAIOptions
{
    public required string Url { get; init; }
    public required string ApiKey { get; init; }
    public required string ChatModelId { get; init; }
    public required string EmbeddingModelId { get; init; }
}