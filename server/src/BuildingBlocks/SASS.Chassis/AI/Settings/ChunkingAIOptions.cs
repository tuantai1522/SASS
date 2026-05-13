namespace SASS.Chassis.AI.Settings;

public sealed class ChunkingAIOptions
{
    public const string SectionName = "ChunkingAI";

    public int MaxTokensPerLine { get; init; }

    public int MaxTokensPerParagraph { get; init; }

    public int OverlapTokens { get; init; }
}