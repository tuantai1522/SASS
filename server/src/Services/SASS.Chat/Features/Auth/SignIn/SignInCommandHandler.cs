using MediatR;
using Microsoft.EntityFrameworkCore;
using SASS.Chassis.Security.PasswordHashing;
using SASS.Chassis.Security.TokenGeneration;
using SASS.Chat.Infrastructure;

namespace SASS.Chat.Features.Auth.SignIn;

internal sealed class SignInCommandHandler(
    ChatDbContext dbContext,
    ITokenProvider tokenProvider,
    IServiceProvider serviceProvider) : IRequestHandler<SignInCommand, SignInResponse>
{
    public async Task<SignInResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .Include(x => x.LocalCredential)
            .AsNoTracking()
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
        
        return !isValidPassword ? 
            throw new UnauthorizedException("Your password is not correct. Please try again!.") : 
            new SignInResponse(tokenProvider.Create(user.Id, user.Email));
    }
}
