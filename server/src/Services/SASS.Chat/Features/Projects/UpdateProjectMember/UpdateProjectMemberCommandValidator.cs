using FluentValidation;

namespace SASS.Chat.Features.Projects.UpdateProjectMember;

internal sealed class UpdateProjectMemberCommandValidator : AbstractValidator<UpdateProjectMemberCommand>
{
    public UpdateProjectMemberCommandValidator()
    {
        RuleFor(x => x.ProjectId)
            .NotEmpty();

        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.Role)
            .IsInEnum();
    }
}
