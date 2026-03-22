using SASS.Chat.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiDocumentationServices();
builder.AddPersistenceServices();

var app = builder.Build();

app.UseApiDocumentation();

app.UseHttpsRedirection();

app.Run();
