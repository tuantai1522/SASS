using MediatR;

namespace SASS.Chat.Features.Projects.DeleteProjectMember;

public sealed record DeleteProjectMemberCommand : IRequest<IdResult>
{
    public Guid ProjectId { get; init; }

    public Guid UserId { get; init; }
}
