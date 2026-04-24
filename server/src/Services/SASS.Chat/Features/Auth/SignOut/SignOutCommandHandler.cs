using MediatR;
using Microsoft.EntityFrameworkCore;
using SASS.Chat.Infrastructure;

namespace SASS.Chat.Features.Auth.SignOut;

internal sealed class SignOutCommandHandler(
    ChatDbContext dbContext,
    ICookieService cookieService): IRequestHandler<SignOutCommand, Unit>
{
    public async Task<Unit> Handle(SignOutCommand command, CancellationToken cancellationToken)
    {
        var refreshToken = cookieService.Get(Application.RefreshTokenCookieName);
        
        cookieService.Delete(Application.RefreshTokenCookieName);
        
        // Update empty token in database
        var token = await dbContext.Set<RefreshToken>()
            .Where(c => c.Token == refreshToken)
            .FirstOrDefaultAsync(cancellationToken);

        if (token is not null)
        {
            var user = await dbContext.Set<User>()
                .Where(c => c.Id == token.UserId)
                .FirstOrDefaultAsync(cancellationToken);

            user?.RotateRefreshToken(token.Id, string.Empty, token.ExpiredAt);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        return default;
    }
}
