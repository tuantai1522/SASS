using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Options;
using SASS.Chassis.AI.Token;
using Microsoft.SemanticKernel.Text;
using SASS.Chassis.AI.Settings;

namespace SASS.Chassis.AI.TextChunkers;

public class MarkdownTextChunker(ITokenCounter tokenCounter, IOptions<ChunkingAIOptions> chunkingAIOptions) : ITextChunker
{
    [Experimental("SKEXP0050")]
    public IList<string> Split(string text)
    {
        TextChunker.TokenCounter counter = tokenCounter.CountTokens;
        
        var lines = TextChunker.SplitMarkDownLines(text, chunkingAIOptions.Value.MaxTokensPerLine, counter);
        var paragraphs = TextChunker.SplitMarkdownParagraphs(lines, chunkingAIOptions.Value.MaxTokensPerParagraph, chunkingAIOptions.Value.OverlapTokens, tokenCounter: counter);

        return paragraphs;
    }
}
