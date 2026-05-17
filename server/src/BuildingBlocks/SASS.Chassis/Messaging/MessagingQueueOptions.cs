namespace SASS.Chassis.Messaging;

public sealed class MessagingQueueOptions
{
    public const string SectionName = "MessagingQueue";

    public string Host { get; init; } = string.Empty;

    public int Port { get; init; } = 5672;

    public string UserName { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;

    public string VirtualHost { get; init; } = "/";

    public bool UseSsl { get; init; }

    public bool AutoProvision { get; init; }

    public bool UseConventionalRouting { get; init; }

    public bool DisableConventionalLocalRouting { get; init; }

    public bool DisableSystemRequestReplyQueueDeclaration { get; init; } = true;
}
