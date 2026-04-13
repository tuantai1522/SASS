using FluentValidation;

namespace SASS.Chat.Features.Projects.CreateProject;

internal sealed class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(64);

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(512);

        RuleFor(x => x.MemberIds)
            .NotNull();

        RuleFor(x => x.LeaderIds)
            .NotNull();

        RuleFor(x => x.MemberIds)
            .Must(HaveUniqueValues)
            .WithMessage("MemberIds cannot contain duplicates.");

        RuleFor(x => x.LeaderIds)
            .Must(HaveUniqueValues)
            .WithMessage("LeaderIds cannot contain duplicates.");
    }

    private static bool HaveUniqueValues(IReadOnlyList<Guid>? values)
    {
        return values is null || values.Count == values.Distinct().Count();
    }
}
