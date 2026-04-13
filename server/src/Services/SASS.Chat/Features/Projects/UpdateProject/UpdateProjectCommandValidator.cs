using FluentValidation;

namespace SASS.Chat.Features.Projects.UpdateProject;

internal sealed class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectCommandValidator()
    {
        RuleFor(x => x.ProjectId)
            .NotEmpty();

        RuleFor(x => x.Key)
            .IsInEnum();

        RuleFor(x => x.Value)
            .NotNull();

        RuleFor(x => x)
            .Custom(ValidateValue);
    }

    private static void ValidateValue(UpdateProjectCommand command, ValidationContext<UpdateProjectCommand> context)
    {
        switch (command.Key)
        {
            case UpdateProjectKey.Title:
                ValidateRequiredString(command.Value, 512, context);
                break;
            case UpdateProjectKey.Code:
                ValidateRequiredString(command.Value, 64, context);
                break;
            case UpdateProjectKey.Description:
            default:
                ValidateRequiredString(command.Value, int.MaxValue, context);
                break;
        }
    }

    private static void ValidateRequiredString(string? value, int maxLength, ValidationContext<UpdateProjectCommand> context)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            context.AddFailure("Value", "Value must not be empty.");
            return;
        }

        if (value.Length > maxLength)
        {
            context.AddFailure("Value", $"Value must not exceed {maxLength} characters.");
        }
    }
}
