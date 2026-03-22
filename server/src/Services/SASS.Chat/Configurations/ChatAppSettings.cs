using SASS.Chassis.Endpoints.Settings;

namespace SASS.Chat.Configurations;

public sealed class ChatAppSettings : AppSettings
{
    public override string Title => "Chat Service API";
    public override string Description => "Manages services for chatting platform, including users, files, messages, and conversations";
    public override ScalarApiContact Contact =>
        new()
        {
            Name = "Tuan Tai",
        };
}
