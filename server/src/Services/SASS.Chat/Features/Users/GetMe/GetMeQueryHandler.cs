using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SASS.Chassis.Security.UserRetrieval;
using SASS.Chassis.Storage;
using SASS.Chassis.Utilities.Guards;
using SASS.Chat.Infrastructure;

namespace SASS.Chat.Features.Users.GetMe;

internal sealed class GetMeQueryHandler(
    ChatDbContext dbContext,
    IOptions<MediaStorageOptions> mediaStorageOptions,
    IServiceProvider serviceProvider,
    IUserProvider userProvider)
    : IRequestHandler<GetMeQuery, GetMeResponse>
{
    public async Task<GetMeResponse> Handle(GetMeQuery request, CancellationToken cancellationToken)
    {
        var userId = userProvider.UserId;

        var mediaStorage = serviceProvider.GetRequiredKeyedService<IMediaStorage>(mediaStorageOptions.Value.Provider);
        
        var user = await dbContext.Users
            .AsNoTracking()
            .Select(x => new
            {
                x.Id,
                x.DisplayName,
                x.Email,
                x.AvatarUrl
            })
            .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

        Guard.Against.NotFound(user, userId);
        
        var response = new GetMeResponse(user.Id, user.Email, user.AvatarUrl != null ? await mediaStorage.GetPresignedUrl(user.AvatarUrl) : null, user.DisplayName);

        return response;
    }
}
