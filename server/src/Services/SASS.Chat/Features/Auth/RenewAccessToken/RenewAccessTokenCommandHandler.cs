using MediatR;
using Microsoft.EntityFrameworkCore;
using SASS.Chassis.Security.TokenGeneration;
using SASS.Chassis.Utilities.Guards;
using SASS.Chat.Infrastructure;

namespace SASS.Chat.Features.Auth.RenewAccessToken;

internal sealed class RenewAccessTokenCommandHandler(
    ChatDbContext dbContext,
    ICookieService cookieService,
    ITokenProvider tokenProvider): IRequestHandler<RenewAccessTokenCommand, RenewAccessTokenResponse>
{
    public async Task<RenewAccessTokenResponse> Handle(RenewAccessTokenCommand command, CancellationToken cancellationToken)
    {
        var refreshToken = cookieService.Get(Application.RefreshTokenCookieName);

        var token = await dbContext.RefreshTokens
            .Where(c => c.Token == refreshToken)
            .FirstOrDefaultAsync(cancellationToken);

        if (token is null || token.ExpiredAt < DateTimeOffset.UtcNow.ToUnixTimeMilliseconds())
        {
            throw new UnauthorizedException("Invalid refresh token");
        }

        var user = await dbContext.Users
            .FirstOrDefaultAsync(user => user.Id == token.UserId, cancellationToken);
        
        Guard.Against.NotFound(user, token.UserId);
        
        string accessToken = tokenProvider.CreateAccessToken(user.Id, user.Email);
        
        var (newRefreshToken, expiredAt) = tokenProvider.CreateRefreshToken();
        
        // Update new token and expiredAt
        user.RotateRefreshToken(token.Id, newRefreshToken, expiredAt);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        // Set cookie of refreshToken in browser
        cookieService.Set(Application.RefreshTokenCookieName, newRefreshToken, expiredAt);
        
        return new RenewAccessTokenResponse(accessToken);
    }
}
