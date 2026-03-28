namespace SASS.Chat.Domain.AggregatesModel.Users;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    
    Task<User> AddAsync(User user, CancellationToken cancellationToken = default);
}
