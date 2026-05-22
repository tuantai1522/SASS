using SASS.Chat.Features.Conversations.CreateMessage;
using SASS.Chat.Features.Files.FileStatusUpdated;
using Task = System.Threading.Tasks.Task;

namespace SASS.Chat.Realtime;

public interface IApplicationNotifier
{
    /// <summary>
    /// Event when message is created
    /// </summary>
    Task MessageCreated(MessageCreatedEventResponse response);
    
    /// <summary>
    /// Event when status of file is updated
    /// </summary>
    Task FileStatusUpdated(FileStatusUpdatedEventResponse response);
}
