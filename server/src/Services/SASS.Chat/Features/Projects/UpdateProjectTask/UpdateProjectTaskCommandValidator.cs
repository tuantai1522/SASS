using FluentValidation;
using FluentValidation.Results;
namespace SASS.Chat.Features.Projects.UpdateProjectTask;

public sealed class UpdateProjectTaskCommandValidator : AbstractValidator<UpdateProjectTaskCommand>
{
    public UpdateProjectTaskCommandValidator()
    {
        RuleFor(x => x.ProjectId)
            .NotEmpty();

        RuleFor(x => x.TaskId)
            .NotEmpty();

        RuleFor(x => x.Key)
            .IsInEnum();

        RuleFor(x => x)
            .Custom(ValidateValue);
    }

    private static void ValidateValue(
        UpdateProjectTaskCommand command,
        ValidationContext<UpdateProjectTaskCommand> context)
    {
        switch (command.Key)
        {
            case UpdateProjectTaskKey.Title:
                ValidateRequiredString(command.Value, 512, context);
                break;

            case UpdateProjectTaskKey.Assignee:
                ValidateOptionalGuid(command.Value, context);
                break;

            case UpdateProjectTaskKey.Status:
            case UpdateProjectTaskKey.Priority:
                ValidateRequiredGuid(command.Value, context);
                break;

            case UpdateProjectTaskKey.StartDate:
            case UpdateProjectTaskKey.DueDate:
                ValidateOptionalDate(command.Value, context);
                break;

            // Don't check description
            case UpdateProjectTaskKey.Description:
                break;
            
            default:
                context.AddFailure(new ValidationFailure(nameof(command.Key), "Invalid update key."));
                break;
        }
    }

    private static void ValidateRequiredString(
        string? value,
        int maxLength,
        ValidationContext<UpdateProjectTaskCommand> context)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            context.AddFailure(nameof(UpdateProjectTaskCommand.Value), "Value must not be empty.");
            return;
        }

        if (value.Length > maxLength)
        {
            context.AddFailure(nameof(UpdateProjectTaskCommand.Value),
                $"Value must not exceed {maxLength} characters.");
        }
    }

    private static void ValidateOptionalGuid(
        string? value,
        ValidationContext<UpdateProjectTaskCommand> context)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        if (!Guid.TryParse(value, out _))
        {
            context.AddFailure(nameof(UpdateProjectTaskCommand.Value), "Value must be a valid GUID.");
        }
    }
    
    private static void ValidateRequiredGuid(
        string? value,
        ValidationContext<UpdateProjectTaskCommand> context)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            context.AddFailure(nameof(UpdateProjectTaskCommand.Value), "Value must be a valid GUID.");
        }

        if (!Guid.TryParse(value, out _))
        {
            context.AddFailure(nameof(UpdateProjectTaskCommand.Value), "Value must be a valid GUID.");
        }
    }

    private static void ValidateOptionalDate(
        string? value,
        ValidationContext<UpdateProjectTaskCommand> context)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        if (!DateOnly.TryParse(value, out _))
        {
            context.AddFailure(nameof(UpdateProjectTaskCommand.Value), "Value must be a valid date.");
        }
    }
}
