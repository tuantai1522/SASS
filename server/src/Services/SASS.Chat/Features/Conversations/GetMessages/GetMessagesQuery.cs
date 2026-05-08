using MediatR;

namespace SASS.Chat.Features.Conversations.GetMessages;

public sealed record GetMessagesQuery(Guid ConversationId, CursorPagedRequest CursorRequest)
    : IRequest<CursorPagedResult<GetMessagesResponse>>;
