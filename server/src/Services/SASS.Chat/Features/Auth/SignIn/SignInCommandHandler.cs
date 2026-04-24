using MediatR;
using Microsoft.EntityFrameworkCore;
using SASS.Chassis.Security.PasswordHashing;
using SASS.Chassis.Security.TokenGeneration;
using SASS.Chat.Infrastructure;
namespace SASS.Chat.Features.Auth.SignIn;

internal sealed class SignInCommandHandler(
    ChatDbContext dbContext,
    ITokenProvider tokenProvider,
    ICookieService cookieService,
    IServiceProvider serviceProvider) : IRequestHandler<SignInCommand, SignInResponse>
{
    public async Task<SignInResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .Include(x => x.LocalCredential)
            .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

        if (user?.LocalCredential is null)
        {
            throw new UnauthorizedException("Your password is not correct. Please try again!.");
        }

        var hashAlgorithm = serviceProvider.GetKeyedService<IPasswordHashAlgorithm>(user.LocalCredential.PasswordAlgo);
        if (hashAlgorithm is null)
        {
            throw new InvalidOperationException($"Password hashing algorithm '{user.LocalCredential.PasswordAlgo}' is not supported.");
        }

        var isValidPassword = hashAlgorithm.Verify(request.Password, user.LocalCredential.PasswordHash);

        if (!isValidPassword)
        {
            throw new UnauthorizedException("Your password is not correct. Please try again!.");
        } 
    
        // 1. Create access token and return for user
        string accessToken = tokenProvider.CreateAccessToken(user.Id, user.Email);
        
        // To store refresh token in database
        var (refreshToken, expiredAt) = tokenProvider.CreateRefreshToken();
        
        user.AddRefreshToken(RefreshToken.Create(user.Id, refreshToken, expiredAt));
        await dbContext.SaveChangesAsync(cancellationToken);
        
        // 2. Set cookie of refreshToken in browser
        cookieService.Set(Application.RefreshTokenCookieName, refreshToken, expiredAt);

        return new SignInResponse(accessToken);
    }
}
