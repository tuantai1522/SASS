namespace SASS.Chassis.Endpoints.Settings;

public class AppSettings
{
    public virtual string DocumentName => "v1";
    public virtual string OpenApiVersion => "1.0.0";
    public virtual bool Enabled => true;
    public virtual bool DevelopmentOnly => true;
    public virtual string EndpointPrefix => "/scalar";
    public virtual string OpenApiRoutePattern => "/openapi/{documentName}.json";
    public virtual string Title => "API Reference";
    public virtual string? Description => null;
    public virtual ScalarApiContact Contact => new();
    public virtual bool EnableJwtBearerSecurity => true;
    public virtual string JwtSecuritySchemeName => "Bearer";
    public virtual string JwtSecurityDescription => "Input JWT token as: Bearer {your token}";
    public virtual string? DefaultJwtToken => null;
    public virtual bool PersistAuthentication => true;
    public virtual IReadOnlyList<string> DocumentNames => ["v1"];
}

public sealed class ScalarApiContact
{
    public string Name { get; init; } = string.Empty;
}
