namespace SASS.Chat.Configurations;

public sealed class SystemOptions
{
    public const string SectionName = "System";
    
    public required string DefaultConversationName { get; init; }
}
