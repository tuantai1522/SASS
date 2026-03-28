using Microsoft.EntityFrameworkCore;

namespace SASS.Chat.Infrastructure.Repositories;

public sealed class UserRepository(ChatDbContext context) : IUserRepository
{
    private readonly ChatDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    
    public IUnitOfWork UnitOfWork => _context;

    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return _context.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }

    public async Task<User> AddAsync(User user, CancellationToken cancellationToken = default)
    {
        var entry = await _context.Users.AddAsync(user, cancellationToken);
        return entry.Entity;
    }
}
