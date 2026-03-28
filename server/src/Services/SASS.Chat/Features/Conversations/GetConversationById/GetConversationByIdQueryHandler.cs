using MediatR;
using SASS.Chassis.Security.UserRetrieval;
using SASS.Chassis.Utilities.Guards;

namespace SASS.Chat.Features.Conversations.GetConversationById;

internal sealed class GetConversationByIdQueryHandler(
    IConversationRepository conversationRepository,
    IUserProvider userProvider
) : IRequestHandler<GetConversationByIdQuery, GetConversationByIdResponse>
{
    public async Task<GetConversationByIdResponse> Handle(GetConversationByIdQuery request, CancellationToken cancellationToken)
    {
        var conversation = await conversationRepository.GetByIdAsync(
            request.ConversationId,
            userProvider.UserId,
            cancellationToken: cancellationToken
        );

        Guard.Against.NotFound(conversation, request.ConversationId);
        
        return new GetConversationByIdResponse(conversation.Id, conversation.Name);
    }
}
