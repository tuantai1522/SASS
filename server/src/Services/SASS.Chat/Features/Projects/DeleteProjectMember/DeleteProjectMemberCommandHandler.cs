using MediatR;
using Microsoft.EntityFrameworkCore;
using SASS.Chassis.Security.UserRetrieval;
using SASS.Chassis.Utilities.Guards;
using SASS.Chat.Infrastructure;

namespace SASS.Chat.Features.Projects.DeleteProjectMember;

internal sealed class DeleteProjectMemberCommandHandler(
    ChatDbContext dbContext,
    IUserProvider userProvider)
    : IRequestHandler<DeleteProjectMemberCommand, IdResult>
{
    public async Task<IdResult> Handle(DeleteProjectMemberCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = userProvider.UserId;

        var project = await dbContext.Projects
            .Include(x => x.Members)
            .FirstOrDefaultAsync(x => x.Id == request.ProjectId, cancellationToken);

        Guard.Against.NotFound(project, request.ProjectId);

        var isLeader = project.Members
            .Any(member => member.UserId == currentUserId && member.Role == ProjectMemberRole.Leader);

        if (!isLeader)
        {
            throw new UnauthorizedException("You do not have permission to remove project members.");
        }

        var member = project.Members
            .FirstOrDefault(x => x.UserId == request.UserId);

        Guard.Against.NotFound(member, request.UserId);

        member.Delete();

        await dbContext.SaveChangesAsync(cancellationToken);

        return new IdResult(member.UserId);
    }
}
