using MediatR;
using Microsoft.EntityFrameworkCore;
using SASS.Chassis.Security.UserRetrieval;
using SASS.Chassis.Utilities.Guards;
using SASS.Chat.Infrastructure;

namespace SASS.Chat.Features.Projects.UpdateProject;

internal sealed class UpdateProjectCommandHandler(
    ChatDbContext dbContext,
    IUserProvider userProvider)
    : IRequestHandler<UpdateProjectCommand, IdResult>
{
    public async Task<IdResult> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var userId = userProvider.UserId;

        var projectAccess = await dbContext.Projects
            .Where(x => x.Id == request.ProjectId)
            .Select(x => new
            {
                Project = x,
                IsLeader = x.Members.Any(m => m.UserId == userId && m.Role == ProjectMemberRole.Leader)
            })
            .FirstOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(projectAccess?.Project, request.ProjectId);

        if (!projectAccess.IsLeader)
        {
            throw new UnauthorizedAccessException("You do not have permission to update this project.");
        }

        var project = projectAccess.Project;

        switch (request.Key)
        {
            case UpdateProjectKey.Title:
                project.ChangeName(request.Value);
                break;
            case UpdateProjectKey.Description:
                project.ChangeDescription(request.Value);
                break;
            case UpdateProjectKey.Code:
                var hasDuplicateCode = await dbContext.Projects
                    .AnyAsync(x => x.OwnerId == project.OwnerId && x.Code == request.Value && x.Id != project.Id, cancellationToken);

                if (hasDuplicateCode)
                {
                    throw new InvalidOperationException("Project code already exists for current owner.");
                }

                project.ChangeCode(request.Value);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(request.Key), request.Key, "Unsupported update key.");
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return new IdResult(project.Id);
    }
}
