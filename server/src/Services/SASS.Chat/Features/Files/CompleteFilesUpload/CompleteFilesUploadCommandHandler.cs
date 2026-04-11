using MediatR;
using Microsoft.EntityFrameworkCore;
using SASS.Chat.Domain.AggregatesModel.Files;
using SASS.Chat.Infrastructure;

namespace SASS.Chat.Features.Files.CompleteFilesUpload;

internal sealed class CompleteFilesUploadCommandHandler(
    ChatDbContext dbContext) : IRequestHandler<CompleteFilesUploadCommand, CompleteFilesUploadResponse>
{
    public async Task<CompleteFilesUploadResponse> Handle(CompleteFilesUploadCommand request, CancellationToken cancellationToken)
    {
        var distinctFileIds = request.FileIds.Distinct().ToArray();

        var files = await dbContext.Files
            .Where(x => distinctFileIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        if (files.Count != distinctFileIds.Length)
        {
            throw new NotFoundException("One or more files were not found in this conversation.");
        }

        foreach (var file in files)
        {
            file.UpdateUploadStatus(UploadStatus.Processing);
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return new CompleteFilesUploadResponse(files.Select(x => x.Id).ToArray());
    }
}
