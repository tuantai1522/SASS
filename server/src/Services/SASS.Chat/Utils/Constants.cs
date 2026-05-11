namespace SASS.Chat.Utils;

public static class Constants
{
    public static class Stream
    {
        public static string Conversation(Guid conversationId)
        {
            return $"{nameof(Conversation)}:{conversationId}";
        }
    }
}