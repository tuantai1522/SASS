namespace SASS.Chassis.AI.TextChunkers;

public interface ITextChunker
{
    IList<string> Split(string text);
}
