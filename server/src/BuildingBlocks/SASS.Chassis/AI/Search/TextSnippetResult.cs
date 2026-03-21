namespace SASS.Chassis.AI.Search;

/// <summary>
/// Get result from embedding (contains record and Score)
/// </summary>
public sealed record TextSnippetResult(TextSnippet TextSnippet, double? Score);
