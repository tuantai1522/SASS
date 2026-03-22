using Microsoft.EntityFrameworkCore;
using SASS.Chat.Domain.AggregatesModel.Conversations;
using SASS.Chat.Domain.AggregatesModel.Files;
using SASS.Chat.Domain.AggregatesModel.Users;
using File = SASS.Chat.Domain.AggregatesModel.Files.File;

namespace SASS.Chat.Infrastructure;

public sealed class ChatDbContext(DbContextOptions<ChatDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<User> Users => Set<User>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<File> Files => Set<File>();
    public DbSet<Conversation> Conversations => Set<Conversation>();
    public DbSet<ConversationFile> ConversationFiles => Set<ConversationFile>();
    public DbSet<Message> Messages => Set<Message>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ChatDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await SaveChangesAsync(cancellationToken);
        return true;
    }
}
