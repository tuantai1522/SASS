using File = SASS.Chat.Domain.AggregatesModel.Files.File;

namespace SASS.Chat.Domain.AggregatesModel.Users;

public sealed class User : Entity, IAggregateRoot
{
    private readonly List<RefreshToken> _refreshTokens = [];
    private readonly List<File> _files = [];
    private readonly List<Conversation> _conversations = [];
    private readonly List<Project> _projects = [];

    private User()
    {
    }

    public static User Create(string email, string? avatarUrl)
    {
        return new User
        {
            Email = email,
            AvatarUrl = avatarUrl
        };
    }

    public string Email { get; private set; } = null!;
    public string? AvatarUrl { get; private set; }

    public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens;
    public IReadOnlyCollection<File> Files => _files;
    public IReadOnlyCollection<Conversation> Conversations => _conversations;
    public IReadOnlyCollection<Project> Projects => _projects;

    public void AddRefreshToken(RefreshToken refreshToken)
    {
        _refreshTokens.Add(refreshToken);
    }

    public void AddFile(File file)
    {
        _files.Add(file);
    }

    public void AddConversation(Conversation conversation)
    {
        _conversations.Add(conversation);
    }
    
    public void AddProject(Project project)
    {
        _projects.Add(project);
    }
}
