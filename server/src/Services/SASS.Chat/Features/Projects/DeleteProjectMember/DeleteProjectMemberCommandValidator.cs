using FluentValidation;

namespace SASS.Chat.Features.Projects.DeleteProjectMember;

internal sealed class DeleteProjectMemberCommandValidator : AbstractValidator<DeleteProjectMemberCommand>
{
    public DeleteProjectMemberCommandValidator()
    {
        RuleFor(x => x.ProjectId)
            .NotEmpty();

        RuleFor(x => x.UserId)
            .NotEmpty();
    }
}
