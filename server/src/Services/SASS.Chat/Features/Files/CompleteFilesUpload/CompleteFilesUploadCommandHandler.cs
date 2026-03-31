using MediatR;
using SASS.Chat.Domain.AggregatesModel.Files;

namespace SASS.Chat.Features.Files.CompleteFilesUpload;

internal sealed class CompleteFilesUploadCommandHandler(
    IFileRepository fileRepository) : IRequestHandler<CompleteFilesUploadCommand, CompleteFilesUploadResponse>
{
    public async Task<CompleteFilesUploadResponse> Handle(CompleteFilesUploadCommand request, CancellationToken cancellationToken)
    {
        var distinctFileIds = request.FileIds.Distinct().ToArray();

        var files = await fileRepository.GetByIdsAsync(
            distinctFileIds,
            cancellationToken: cancellationToken
        );

        if (files.Count != distinctFileIds.Length)
        {
            throw new NotFoundException("One or more files were not found in this conversation.");
        }

        foreach (var file in files)
        {
            file.UpdateUploadStatus(UploadStatus.Processing);
        }

        await fileRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new CompleteFilesUploadResponse(files.Select(x => x.Id).ToArray());
    }
}
