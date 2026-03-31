using FluentValidation;

namespace SASS.Chat.Features.Files.CompleteFilesUpload;

internal sealed class CompleteFilesUploadCommandValidator : AbstractValidator<CompleteFilesUploadCommand>
{
    public CompleteFilesUploadCommandValidator()
    {
        RuleFor(x => x.ConversationId)
            .NotEmpty();

        RuleFor(x => x.FileIds)
            .NotEmpty()
            .Must(x => x.Count <= 20)
            .WithMessage("Maximum 20 files per request.");

        RuleForEach(x => x.FileIds)
            .NotEmpty();
    }
}
