using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel.Text;
using SASS.Chassis.AI.Settings;
using SASS.Chassis.AI.Token;

namespace SASS.Chassis.AI.TextChunkers;

public class DefaultTextChunker(ITokenCounter tokenCounter, IOptions<ChunkingAIOptions> chunkingAIOptions) : ITextChunker
{
    [Experimental("SKEXP0050")]
    public IList<string> Split(string text)
    {
        TextChunker.TokenCounter counter = tokenCounter.CountTokens;
        
        var lines = TextChunker.SplitPlainTextLines(text, chunkingAIOptions.Value.MaxTokensPerLine, counter);
        var paragraphs = TextChunker.SplitPlainTextParagraphs(lines, chunkingAIOptions.Value.MaxTokensPerParagraph, chunkingAIOptions.Value.OverlapTokens, tokenCounter: counter);

        return paragraphs;
    }
}
