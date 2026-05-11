using SASS.Chat.Realtime;

namespace SASS.Chat.Features.Conversations.CreateMessage;

public sealed record MessageCreatedEventResponse(
    Guid ConversationId,
    Guid Id,
    string Content,
    long CreatedAt,
    Guid? SenderId,
    string? DisplayName,
    string? AvatarUrl) : IApplicationEvent;
