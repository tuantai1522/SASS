using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SASS.Chassis.Security.PasswordHashing;
using SASS.Chassis.Security.Settings;
using SASS.Chat.Infrastructure;

namespace SASS.Chat.Features.Auth.SignUp;

internal sealed class SignUpCommandHandler(
    ChatDbContext dbContext,
    IOptions<PasswordHashingOptions> passwordHashingOptions,
    IServiceProvider serviceProvider) : IRequestHandler<SignUpCommand, IdResult>
{
    public async Task<IdResult> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var emailExists = await dbContext.Users
            .AnyAsync(x => x.Email == request.Email, cancellationToken);

        if (emailExists)
        {
            throw new ConflictException("Email already exists.");
        }
        
        var displayNameExists = await dbContext.Users
            .AnyAsync(x => x.DisplayName == request.DisplayName, cancellationToken);

        if (displayNameExists)
        {
            throw new ConflictException("Name already exists.");
        }

        var algorithmName = passwordHashingOptions.Value.CurrentAlgorithm;
        var hashAlgorithm = serviceProvider.GetRequiredKeyedService<IPasswordHashAlgorithm>(algorithmName);
        var hashedPassword = hashAlgorithm.Hash(request.Password);

        var user = User.Create(request.Email, null, request.DisplayName.Trim());

        user.AddLocalCredential(LocalCredential.Create(user.Id, hashedPassword, algorithmName));

        await dbContext.Users.AddAsync(user, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new IdResult(user.Id);
    }
}
