using SASS.Chat.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiDocumentationServices();

builder.AddApplicationServices();

var app = builder.Build();

app.UseExceptionHandler();

app.UseApiDocumentation();
var apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(ApiVersions.V1)
    .ReportApiVersions()
    .Build();

app.MapEndpoints(apiVersionSet);

app.UseHttpsRedirection();

app.UseAuthentication(); 
app.UseAuthorization();

app.Run();
