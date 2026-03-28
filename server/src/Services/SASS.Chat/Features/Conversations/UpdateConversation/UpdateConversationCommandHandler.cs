using MediatR;
using SASS.Chassis.Security.UserRetrieval;
using SASS.Chassis.Utilities.Guards;

namespace SASS.Chat.Features.Conversations.UpdateConversation;

internal sealed class UpdateConversationCommandHandler(
    IConversationRepository conversationRepository,
    IUserProvider userProvider
) : IRequestHandler<UpdateConversationCommand, IdResult>
{
    public async Task<IdResult> Handle(UpdateConversationCommand request, CancellationToken cancellationToken)
    {
        var conversation = await conversationRepository.GetByIdAsync(
            request.ConversationId,
            userProvider.UserId,
            asTracking: true,
            cancellationToken: cancellationToken
        );

        Guard.Against.NotFound(conversation, request.ConversationId);

        conversation.Rename(request.Name);
        await conversationRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new IdResult(conversation.Id);
    }
}
