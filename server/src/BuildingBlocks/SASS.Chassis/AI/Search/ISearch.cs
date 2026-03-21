namespace SASS.Chassis.AI.Search;

public interface ISearch
{
    Task<IReadOnlyList<TextSnippetResult>> SearchAsync(
        string text,
        int maxResults = 20,
        CancellationToken cancellationToken = default
    );
}
