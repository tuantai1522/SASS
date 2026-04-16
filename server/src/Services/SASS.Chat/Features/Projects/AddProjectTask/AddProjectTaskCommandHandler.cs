using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using SASS.Chassis.Security.UserRetrieval;
using SASS.Chassis.Utilities.Guards;
using SASS.Chat.Infrastructure;
using TaskPriorityEntity = SASS.Chat.Domain.AggregatesModel.Projects.TaskPriority;
using TaskStatusEntity = SASS.Chat.Domain.AggregatesModel.Projects.TaskStatus;
using ProjectTask = SASS.Chat.Domain.AggregatesModel.Projects.Task;

namespace SASS.Chat.Features.Projects.AddProjectTask;

internal sealed class AddProjectTaskCommandHandler(
    ChatDbContext dbContext,
    IUserProvider userProvider)
    : IRequestHandler<AddProjectTaskCommand, IdResult>
{
    private const int MaxTaskCodeGenerationRetries = 3;
    private const string UniqueViolationSqlState = "23505";
    private const string TaskCodeConstraintName = "ux_tasks_project_id_code_active";

    public async Task<IdResult> Handle(AddProjectTaskCommand request, CancellationToken cancellationToken)
    {
        var userId = userProvider.UserId;

        var statusExists = await dbContext.TaskStatuses
            .AnyAsync(x => x.Id == request.StatusId, cancellationToken);

        if (!statusExists)
        {
            throw NotFoundException.For<TaskStatusEntity>(request.StatusId);
        }

        var priorityExists = await dbContext.TaskPriorities
            .AnyAsync(x => x.Id == request.PriorityId, cancellationToken);

        if (!priorityExists)
        {
            throw NotFoundException.For<TaskPriorityEntity>(request.PriorityId);
        }

        for (var attempt = 0; attempt < MaxTaskCodeGenerationRetries; attempt++)
        {
            var project = await dbContext.Projects
                .Include(x => x.Members)
                .FirstOrDefaultAsync(x => x.Id == request.ProjectId, cancellationToken);

            Guard.Against.NotFound(project, request.ProjectId);

            var isLeader = project.Members
                .Any(member => member.UserId == userId && member.Role == ProjectMemberRole.Leader);

            if (!isLeader)
            {
                throw new UnauthorizedAccessException("You do not have permission to create tasks in this project.");
            }

            if (request.AssigneeId.HasValue)
            {
                var assigneeExists = project.Members
                    .Any(member => member.UserId == request.AssigneeId.Value);

                if (!assigneeExists)
                {
                    throw new NotFoundException($"Project assignee with id {request.AssigneeId.Value} not found.");
                }
            }

            var taskCode = project.GetNextTaskCode();
            var task = ProjectTask.Create(
                project.Id,
                taskCode,
                request.Title,
                request.Description,
                request.StatusId,
                request.PriorityId,
                request.AssigneeId,
                request.StartDate,
                request.DueDate);

            project.AddTask(task);

            try
            {
                await dbContext.SaveChangesAsync(cancellationToken);

                return new IdResult(task.Id);
            }
            catch (DbUpdateException exception) when (IsTaskCodeConflict(exception) && attempt < MaxTaskCodeGenerationRetries - 1)
            {
                dbContext.ChangeTracker.Clear();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (var entry in ex.Entries)
                {
                    Console.WriteLine(entry.Entity.GetType().Name);
                }

                throw;
            }
        }

        throw new InvalidOperationException("Unable to create task due to concurrent code generation conflicts. Please try again.");
    }

    private static bool IsTaskCodeConflict(DbUpdateException exception)
    {
        return exception.InnerException is PostgresException postgresException
               && postgresException.SqlState == UniqueViolationSqlState
               && string.Equals(postgresException.ConstraintName, TaskCodeConstraintName, StringComparison.Ordinal);
    }
}
