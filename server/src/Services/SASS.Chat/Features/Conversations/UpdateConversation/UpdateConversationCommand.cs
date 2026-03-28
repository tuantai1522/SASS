using MediatR;

namespace SASS.Chat.Features.Conversations.UpdateConversation;

public sealed record UpdateConversationCommand(Guid ConversationId, string Name) : IRequest<IdResult>;
