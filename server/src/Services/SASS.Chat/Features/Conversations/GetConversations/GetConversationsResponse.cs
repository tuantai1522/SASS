namespace SASS.Chat.Features.Conversations.GetConversations;

public sealed record GetConversationsResponse(Guid Id, string Name, long CreatedAt, long LastMessageUpdatedAt);
