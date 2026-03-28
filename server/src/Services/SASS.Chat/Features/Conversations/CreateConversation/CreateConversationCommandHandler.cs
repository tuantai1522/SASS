using MediatR;
using Microsoft.Extensions.Options;
using SASS.Chassis.Security.UserRetrieval;
using SASS.Chat.Configurations;

namespace SASS.Chat.Features.Conversations.CreateConversation;

internal sealed class CreateConversationCommandHandler(
    IConversationRepository conversationRepository,
    IUserProvider userProvider,
    IOptions<SystemOptions> options) : IRequestHandler<CreateConversationCommand, IdResult>
{
    public async Task<IdResult> Handle(CreateConversationCommand request, CancellationToken cancellationToken)
    {
        var conversation = Conversation.Create(userProvider.UserId, options.Value.DefaultConversationName);

        await conversationRepository.AddAsync(conversation, cancellationToken);
        await conversationRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new IdResult(conversation.Id);
    }
}
