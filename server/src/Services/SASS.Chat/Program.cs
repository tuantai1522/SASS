using SASS.Chat.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();

var app = builder.Build();

app.UseExceptionHandler();

app.UseApiDocumentation();
app.UseAuthentication();
app.UseAuthorization();

var apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(ApiVersions.V1)
    .ReportApiVersions()
    .Build();

app.MapEndpoints(apiVersionSet);

app.UseDefaultCors();

app.UseHttpsRedirection();

app.UseAuthentication(); 
app.UseAuthorization();

app.Run();
