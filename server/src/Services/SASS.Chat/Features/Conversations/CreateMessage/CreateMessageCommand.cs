using MediatR;

namespace SASS.Chat.Features.Conversations.CreateMessage;

public sealed record CreateMessageCommand(Guid ConversationId, string Content) : IRequest<IdResult>;
