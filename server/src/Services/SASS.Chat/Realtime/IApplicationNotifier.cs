using SASS.Chat.Features.Conversations.CreateMessage;
using Task = System.Threading.Tasks.Task;

namespace SASS.Chat.Realtime;

public interface IApplicationNotifier
{
    Task MessageCreated(MessageCreatedEventResponse response);
}
