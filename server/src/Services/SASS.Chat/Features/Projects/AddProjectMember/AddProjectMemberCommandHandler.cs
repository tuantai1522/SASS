using MediatR;
using Microsoft.EntityFrameworkCore;
using SASS.Chassis.Security.UserRetrieval;
using SASS.Chassis.Utilities.Guards;
using SASS.Chat.Infrastructure;

namespace SASS.Chat.Features.Projects.AddProjectMember;

internal sealed class AddProjectMemberCommandHandler(
    ChatDbContext dbContext,
    IUserProvider userProvider)
    : IRequestHandler<AddProjectMemberCommand, IdResult>
{
    public async Task<IdResult> Handle(AddProjectMemberCommand request, CancellationToken cancellationToken)
    {
        var userId = userProvider.UserId;

        var project = await dbContext.Projects
            .Include(x => x.Members)
            .FirstOrDefaultAsync(x => x.Id == request.ProjectId, cancellationToken);

        Guard.Against.NotFound(project, request.ProjectId);

        var isLeader = project.Members
            .Any(member => member.UserId == userId && member.Role == ProjectMemberRole.Leader);

        if (!isLeader)
        {
            throw new UnauthorizedAccessException("You do not have permission to add members to this project.");
        }

        var memberExists = project.Members
            .Any(member => member.UserId == request.UserId);

        if (memberExists)
        {
            throw new InvalidOperationException("Project member already exists.");
        }

        project.AddMembers([
            ProjectMember.Create(project.Id, request.UserId, request.Role)
        ]);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new IdResult(request.UserId);
    }
}
