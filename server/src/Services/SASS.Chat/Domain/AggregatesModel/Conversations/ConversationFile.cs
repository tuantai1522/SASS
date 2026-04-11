using SASS.Chat.Domain.Exceptions;
using File = SASS.Chat.Domain.AggregatesModel.Files.File;

namespace SASS.Chat.Domain.AggregatesModel.Conversations;

public sealed class ConversationFile
{
    private ConversationFile()
    {
    }

    public static ConversationFile Create(Guid conversationId, Guid fileId)
    {
        return new ConversationFile
        {
            ConversationId = conversationId,
            FileId = fileId,
            Active = true
        };
    }

    public Guid ConversationId { get; private set; }
    public Conversation Conversation { get; private set; } = null!;

    public Guid FileId { get; private set; }
    public File File { get; private set; } = null!;

    public bool Active { get; private set; }

    public void SetActive(bool active)
    {
        Active = active;
    }
}
