using MediatR;
using Microsoft.EntityFrameworkCore;
using SASS.Chassis.Security.UserRetrieval;
using SASS.Chassis.Utilities.Guards;
using SASS.Chat.Infrastructure;
namespace SASS.Chat.Features.Projects.DeleteProjectTask;

internal sealed class DeleteProjectTaskCommandHandler(
    ChatDbContext dbContext,
    IUserProvider userProvider)
    : IRequestHandler<DeleteProjectTaskCommand, IdResult>
{
    public async Task<IdResult> Handle(DeleteProjectTaskCommand request, CancellationToken cancellationToken)
    {
        // 1. Get current user
        var userId = userProvider.UserId;
        
        // 2. Get project by id
        var projectAccess = await dbContext.Projects
            .Where(x => x.Id == request.ProjectId)
            .Select(x => new
            {
                Project = x,
                IsLeader = x.Members.Any(m => m.UserId == userId && m.Role == ProjectMemberRole.Leader),
            })
            .FirstOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(projectAccess?.Project, request.ProjectId);

        if (!projectAccess.IsLeader)
        {
            throw new UnauthorizedAccessException("You do not have permission to delete this task.");
        }
        
        // 2. Get task by id
        var task = await dbContext.Tasks
            .FirstOrDefaultAsync(x => x.Id == request.TaskId, cancellationToken: cancellationToken);
        
        Guard.Against.NotFound(task, request.TaskId);

        // To delete this task
        task.Delete();
        
        await dbContext.SaveChangesAsync(cancellationToken);

        return new IdResult(task.Id);
    }
}
