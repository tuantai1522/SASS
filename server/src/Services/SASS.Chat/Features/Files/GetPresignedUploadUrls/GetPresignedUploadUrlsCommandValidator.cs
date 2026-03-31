using FluentValidation;

namespace SASS.Chat.Features.Files.GetPresignedUploadUrls;

internal sealed class GetPresignedUploadUrlsCommandValidator : AbstractValidator<GetPresignedUploadUrlsCommand>
{
    public GetPresignedUploadUrlsCommandValidator()
    {
        RuleFor(x => x.ConversationId)
            .NotEmpty();

        RuleFor(x => x.Request.Files)
            .NotEmpty()
            .Must(files => files.Count <= 20)
            .WithMessage("Maximum 20 files per request.");
    }
}
