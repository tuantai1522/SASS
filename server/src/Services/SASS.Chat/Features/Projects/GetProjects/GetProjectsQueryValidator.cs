using FluentValidation;

namespace SASS.Chat.Features.Projects.GetProjects;

internal sealed class GetProjectsQueryValidator : AbstractValidator<GetProjectsQuery>
{
    public GetProjectsQueryValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(Pagination.DefaultPageIndex);

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(Pagination.DefaultPageSize);

        RuleFor(x => x.Order)
            .IsInEnum();
        
        RuleFor(x => x.OrderBy)
            .IsInEnum();
    }
}
