using Microsoft.EntityFrameworkCore;
using SASS.Chassis.Repository;
using File = SASS.Chat.Domain.AggregatesModel.Files.File;
using ProjectTaskStatus = SASS.Chat.Domain.AggregatesModel.Projects.TaskStatus;
using Task = SASS.Chat.Domain.AggregatesModel.Projects.Task;

namespace SASS.Chat.Infrastructure;

public sealed class ChatDbContext(DbContextOptions<ChatDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<User> Users => Set<User>();
    public DbSet<LocalCredential> LocalCredentials => Set<LocalCredential>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<File> Files => Set<File>();
    public DbSet<Conversation> Conversations => Set<Conversation>();
    public DbSet<ConversationFile> ConversationFiles => Set<ConversationFile>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ProjectMember> ProjectMembers => Set<ProjectMember>();
    public DbSet<Task> Tasks => Set<Task>();
    public DbSet<ProjectTaskStatus> TaskStatuses => Set<ProjectTaskStatus>();
    public DbSet<TaskPriority> TaskPriorities => Set<TaskPriority>();

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
