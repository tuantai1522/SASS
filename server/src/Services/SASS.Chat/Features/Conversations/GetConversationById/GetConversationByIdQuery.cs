using MediatR;

namespace SASS.Chat.Features.Conversations.GetConversationById;

public sealed record GetConversationByIdQuery(Guid ConversationId) : IRequest<GetConversationByIdResponse>;
