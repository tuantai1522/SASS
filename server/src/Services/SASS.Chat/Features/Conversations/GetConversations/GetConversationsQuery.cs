using MediatR;

namespace SASS.Chat.Features.Conversations.GetConversations;

public sealed record GetConversationsQuery(CursorPagedRequest CursorRequest)
    : IRequest<CursorPagedResponse<GetConversationsResponse>>;
