using MediatR;
using Microsoft.EntityFrameworkCore;
using SASS.Chassis.Security.UserRetrieval;
using SASS.Chassis.Utilities.Guards;
using SASS.Chat.Infrastructure;

namespace SASS.Chat.Features.Projects.DeleteProject;

internal sealed class DeleteProjectCommandHandler(
    ChatDbContext dbContext,
    IUserProvider userProvider)
    : IRequestHandler<DeleteProjectCommand, IdResult>
{
    public async Task<IdResult> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var userId = userProvider.UserId;

        var project = await dbContext.Projects
            .Include(x => x.Members)
            .Include(x => x.Tasks)
            .FirstOrDefaultAsync(x => x.Id == request.ProjectId, cancellationToken);

        Guard.Against.NotFound(project, request.ProjectId);

        var canDeleteProject = project.Members
            .Any(member => member.UserId == userId && member.Role == ProjectMemberRole.Leader);

        if (!canDeleteProject)
        {
            throw new UnauthorizedAccessException("You are not authorized to delete this project.");
        }

        project.Delete();

        await dbContext.SaveChangesAsync(cancellationToken);

        return new IdResult(project.Id);
    }
}
