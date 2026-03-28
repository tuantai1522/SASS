namespace SASS.Chat.Configurations;

public sealed class GoogleAuthOptions
{
    public string ClientId { get; init; } = null!;
    public string ClientSecret { get; init; } = null!;
    public string RedirectUri { get; init; } = null!;
    public string Scope { get; init; } = null!;
    public string GoogleUrl { get; init; } = null!;
    public string GoogleAuthTokenEndpoint { get; init; } = null!;
    public string GoogleContactInfoEndpoint { get; init; } = null!;
}
