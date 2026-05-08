namespace SASS.Chat.Features.Conversations.GetMessages;

public sealed record GetMessagesResponse(
    Guid Id, string Content, long CreatedAt, 
    bool IsMe, Guid? SenderId, string? DisplayName, string? AvatarUrl);
