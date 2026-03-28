using MediatR;

namespace SASS.Chat.Features.Conversations.CreateConversation;

public sealed record CreateConversationCommand : IRequest<IdResult>;
