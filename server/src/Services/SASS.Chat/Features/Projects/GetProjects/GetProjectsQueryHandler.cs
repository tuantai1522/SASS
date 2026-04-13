using MediatR;
using Microsoft.EntityFrameworkCore;
using SASS.Chassis.Security.UserRetrieval;
using SASS.Chat.Infrastructure;

namespace SASS.Chat.Features.Projects.GetProjects;

internal sealed class GetProjectsQueryHandler(
    ChatDbContext dbContext,
    IUserProvider userProvider)
    : IRequestHandler<GetProjectsQuery, PagedResult<GetProjectsItemResponse>>
{
    public async Task<PagedResult<GetProjectsItemResponse>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        var page = request.Page;
        var pageSize = request.PageSize;
        var search = request.Search;
        var orderBy = request.OrderBy;
        var order = request.Order;
        var userId = userProvider.UserId;

        var query = dbContext.Projects
            .AsNoTracking()
            .Where(x => x.OwnerId == userId || x.Members.Any(m => m.UserId == userId));

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(x => EF.Functions.ILike(x.Title, $"%{search}%"));
        }

        query = (orderBy, order) switch
        {
            (ProjectOrderBy.Title, Order.Asc) => query.OrderBy(x => x.Title).ThenBy(x => x.Id),
            (ProjectOrderBy.Title, Order.Desc) => query.OrderByDescending(x => x.Title).ThenByDescending(x => x.Id),
            (ProjectOrderBy.CreatedAt, Order.Asc) => query.OrderBy(x => x.CreatedAt).ThenBy(x => x.Id),
            _ => query.OrderByDescending(x => x.CreatedAt).ThenByDescending(x => x.Id)
        };

        var totalItems = await query.LongCountAsync(cancellationToken);

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new GetProjectsItemResponse
            {
                Id = x.Id,
                Code = x.Code,
                Title = x.Title,
                Description = x.Description,
                CreatedAt = x.CreatedAt,
                Role = x.Members
                    .Where(m => m.UserId == userId)
                    .Select(m => m.Role.ToString())
                    .FirstOrDefault() ?? nameof(ProjectMemberRole.Member)
            })
            .ToListAsync(cancellationToken);

        return new PagedResult<GetProjectsItemResponse>(items, page, pageSize, totalItems);
    }
}
