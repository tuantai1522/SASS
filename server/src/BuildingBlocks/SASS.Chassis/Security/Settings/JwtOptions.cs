namespace SASS.Chassis.Security.Settings;

public sealed class JwtOptions
{
    public required string AccessTokenKey { get; init; }
    public required string Secret { get; init; }
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    
    public required int ExpiredAccessToken { get; set; }
}
