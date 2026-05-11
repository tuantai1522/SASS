namespace SASS.Chat.Realtime;

internal static class Extensions
{
    extension(IHostApplicationBuilder builder)
    {
        public void AddRealtimeServices()
        {
            builder.Services.AddSignalR();
        }
    }

    extension(WebApplication app)
    {
        public void MapRealtimeEndpoints()
        {
            app.MapHub<ApplicationNotifier>("hubs/application");
        }
    }
}
