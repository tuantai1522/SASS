using FluentValidation;

namespace SASS.Chat.Features.Files.GetPresignedDownloadUrl;

internal sealed class GetPresignedDownloadUrlValidator : AbstractValidator<GetPresignedDownloadUrlQuery>
{
    public GetPresignedDownloadUrlValidator()
    {
        RuleFor(x => x.ConversationId).NotEmpty();
        RuleFor(x => x.FileId).NotEmpty();
    }
}
