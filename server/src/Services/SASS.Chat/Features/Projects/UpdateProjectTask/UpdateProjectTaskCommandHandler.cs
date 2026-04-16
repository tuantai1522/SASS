using MediatR;
using Microsoft.EntityFrameworkCore;
using SASS.Chassis.Security.UserRetrieval;
using SASS.Chassis.Utilities.Guards;
using SASS.Chat.Infrastructure;
using TaskStatus = SASS.Chat.Domain.AggregatesModel.Projects.TaskStatus;
using TaskPriority = SASS.Chat.Domain.AggregatesModel.Projects.TaskPriority;
namespace SASS.Chat.Features.Projects.UpdateProjectTask;

internal sealed class UpdateProjectTaskCommandHandler(
    ChatDbContext dbContext,
    IUserProvider userProvider)
    : IRequestHandler<UpdateProjectTaskCommand, IdResult>
{
    public async Task<IdResult> Handle(UpdateProjectTaskCommand request, CancellationToken cancellationToken)
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
                Participants = x.Members
            })
            .FirstOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(projectAccess?.Project, request.ProjectId);

        // 2. Get task by id
        var task = await dbContext.Tasks
            .FirstOrDefaultAsync(x => x.Id == request.TaskId, cancellationToken: cancellationToken);
        
        Guard.Against.NotFound(task, request.TaskId);

        switch (request.Key)
        {
            case UpdateProjectTaskKey.Title:
                if (!projectAccess.IsLeader)
                {
                    throw new UnauthorizedAccessException("You do not have permission to update this task.");
                }
                
                task.ChangeTitle(request.Value);
                break;
            case UpdateProjectTaskKey.Description:
                if (!projectAccess.IsLeader)
                {
                    throw new UnauthorizedAccessException("You do not have permission to update this task.");
                }
                
                task.ChangeDescription(request.Value);
                break;
            case UpdateProjectTaskKey.Assignee:
                if (!projectAccess.IsLeader)
                {
                    throw new UnauthorizedAccessException("You do not have permission to update this task.");
                }
                
                // To verify this user must be in project
                var participantExists = projectAccess.Participants.Any(x => x.UserId.ToString() == request.Value);
                Guard.Against.NotFound<TaskPriority>(participantExists, Guid.Parse(request.Value));
                
                task.ChangeAssignee(Guid.Parse(request.Value));

                break;
            case UpdateProjectTaskKey.Status:
                var statusExists = await dbContext.TaskStatuses
                    .AnyAsync(x => x.Id == Guid.Parse(request.Value), cancellationToken);

                Guard.Against.NotFound<TaskStatus>(statusExists, Guid.Parse(request.Value));
                task.ChangeStatus(Guid.Parse(request.Value));
                
                break;
            
            case UpdateProjectTaskKey.Priority:
                if (!projectAccess.IsLeader)
                {
                    throw new UnauthorizedAccessException("You do not have permission to update this task.");
                }
                
                var priorityExists = await dbContext.TaskPriorities
                    .AnyAsync(x => x.Id == Guid.Parse(request.Value), cancellationToken);

                Guard.Against.NotFound<TaskPriority>(priorityExists, Guid.Parse(request.Value));
                
                task.ChangePriority(Guid.Parse(request.Value));
                
                break;
            
            case UpdateProjectTaskKey.StartDate:
                if (!projectAccess.IsLeader)
                {
                    throw new UnauthorizedAccessException("You do not have permission to update this task.");
                }
                
                task.ChangeStartDate(DateOnly.Parse(request.Value));
                
                break;
            case UpdateProjectTaskKey.DueDate:
                if (!projectAccess.IsLeader)
                {
                    throw new UnauthorizedAccessException("You do not have permission to update this task.");
                }
                
                task.ChangeDueDate(DateOnly.Parse(request.Value));
                
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(request.Key), request.Key, "Unsupported update key.");
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return new IdResult(task.Id);
    }
}
