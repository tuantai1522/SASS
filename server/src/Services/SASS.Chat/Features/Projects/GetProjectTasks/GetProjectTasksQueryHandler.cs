using MediatR;
using Microsoft.EntityFrameworkCore;
using SASS.Chassis.Security.UserRetrieval;
using SASS.Chassis.Utilities.Guards;
using SASS.Chat.Infrastructure;
using TaskEntity = SASS.Chat.Domain.AggregatesModel.Projects.Task;

namespace SASS.Chat.Features.Projects.GetProjectTasks;

internal sealed class GetProjectTasksQueryHandler(
    ChatDbContext dbContext,
    IUserProvider userProvider)
    : IRequestHandler<GetProjectTasksQuery, PagedResult<GetProjectTasksItemResponse>>
{
    public async Task<PagedResult<GetProjectTasksItemResponse>> Handle(GetProjectTasksQuery request, CancellationToken cancellationToken)
    {
        var userId = userProvider.UserId;

        var projectExists = await dbContext.Projects
            .AsNoTracking()
            .AnyAsync(x => x.Id == request.ProjectId && x.Members.Any(m => m.UserId == userId), cancellationToken: cancellationToken);

        Guard.Against.NotFound(projectExists, request.ProjectId);

        var query = dbContext.Tasks
            .AsNoTracking()
            .Where(x => x.ProjectId == request.ProjectId);

        if (request.StatusIds.Count > 0)
        {
            query = query.Where(x => request.StatusIds.Contains(x.StatusId));
        }

        if (request.AssigneeIds.Count > 0)
        {
            query = query.Where(x => x.AssigneeId.HasValue && request.AssigneeIds.Contains(x.AssigneeId.Value));
        }

        if (request.PriorityIds.Count > 0)
        {
            query = query.Where(x => request.PriorityIds.Contains(x.PriorityId));
        }

        query = ApplySearch(query, request.Search);
        query = ApplyOrdering(query, request.OrderBy, request.Order);

        var totalItems = await query.LongCountAsync(cancellationToken);

        var rawItems = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new
            {
                x.Id,
                x.Code,
                x.Title,
                x.Description,
                
                x.StatusId,
                StatusName = x.Status.Name,
                
                x.PriorityId,
                PriorityName = x.Priority.Name,
                
                x.AssigneeId,
                AssigneeName = x.Assignee != null ? x.Assignee.DisplayName : null,
                
                x.StartDate,
                x.DueDate,
                x.CreatedAt
            })
            .ToListAsync(cancellationToken);

        var items = rawItems
            .Select(x => new GetProjectTasksItemResponse
            {
                Id = x.Id,
                Code = x.Code.ToString("D4"),
                Title = x.Title,
                Description = x.Description,
                
                StatusId = x.StatusId,
                StatusName = x.StatusName,
                
                PriorityId = x.PriorityId,
                PriorityName = x.PriorityName,
                
                AssigneeId = x.AssigneeId,
                AssigneeName = x.AssigneeName,
                
                StartDate = x.StartDate,
                DueDate = x.DueDate,
                CreatedAt = x.CreatedAt
            })
            .ToList();

        return new PagedResult<GetProjectTasksItemResponse>(items, request.Page, request.PageSize, totalItems);
    }

    private static IQueryable<TaskEntity> ApplySearch(IQueryable<TaskEntity> query, string? search)
    {
        if (string.IsNullOrWhiteSpace(search))
        {
            return query;
        }

        var normalizedSearch = search.Trim();
        var parsedCode = TryParseTaskCode(normalizedSearch);

        return query.Where(x =>
            EF.Functions.ILike(x.Title, $"%{normalizedSearch}%") ||
            (parsedCode.HasValue && x.Code == parsedCode.Value));
    }

    private static IQueryable<TaskEntity> ApplyOrdering(IQueryable<TaskEntity> query, GetProjectTasksOrderBy orderBy, Order order)
    {
        return (orderBy, order) switch
        {
            (GetProjectTasksOrderBy.Title, Order.Asc) => query.OrderBy(x => x.Title).ThenBy(x => x.Id),
            (GetProjectTasksOrderBy.Title, Order.Desc) => query.OrderByDescending(x => x.Title).ThenByDescending(x => x.Id),
            (GetProjectTasksOrderBy.Priority, Order.Asc) => query.OrderBy(x => x.Priority.Order).ThenBy(x => x.Id),
            (GetProjectTasksOrderBy.Priority, Order.Desc) => query.OrderByDescending(x => x.Priority.Order).ThenByDescending(x => x.Id),
            (GetProjectTasksOrderBy.DueDate, Order.Asc) => query.OrderBy(x => x.DueDate == null).ThenBy(x => x.DueDate).ThenBy(x => x.Id),
            _ => query.OrderBy(x => x.DueDate == null).ThenByDescending(x => x.DueDate).ThenByDescending(x => x.Id)
        };
    }

    private static int? TryParseTaskCode(string search)
    {
        var normalizedCode = search.TrimStart('0');

        if (normalizedCode.Length == 0)
        {
            normalizedCode = "0";
        }

        return int.TryParse(normalizedCode, out var code) ? code : null;
    }
}
