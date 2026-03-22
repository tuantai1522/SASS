using SASS.Chat.Domain.AggregatesModel.Conversations;
using SASS.Chat.Domain.Exceptions;
using File = SASS.Chat.Domain.AggregatesModel.Files.File;

namespace SASS.Chat.Domain.AggregatesModel.Users;

public sealed class User : Entity, IAggregateRoot
{
    private readonly List<RefreshToken> _refreshTokens = [];
    private readonly List<File> _files = [];
    private readonly List<Conversation> _conversations = [];

    private User()
    {
    }

    public static User Create(string email, string avatarUrl)
    {
        EnsureRequiredText(email, nameof(email), 512);
        EnsureRequiredText(avatarUrl, nameof(avatarUrl), 512);

        return new User
        {
            Email = email,
            AvatarUrl = avatarUrl
        };
    }

    public string Email { get; private set; } = null!;
    public string AvatarUrl { get; private set; } = null!;

    public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens;
    public IReadOnlyCollection<File> Files => _files;
    public IReadOnlyCollection<Conversation> Conversations => _conversations;

    public void AddRefreshToken(RefreshToken refreshToken)
    {
        if (refreshToken.UserId != Id)
        {
            throw new ChatDomainException("Refresh token user id does not match current user.");
        }

        _refreshTokens.Add(refreshToken);
    }

    public void AddFile(File file)
    {
        if (file.UserId != Id)
        {
            throw new ChatDomainException("File user id does not match current user.");
        }

        _files.Add(file);
    }

    public void AddConversation(Conversation conversation)
    {
        if (conversation.UserId != Id)
        {
            throw new ChatDomainException("Conversation user id does not match current user.");
        }

        _conversations.Add(conversation);
    }

    private static void EnsureRequiredText(string value, string fieldName, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ChatDomainException($"{fieldName} is required.");
        }

        if (value.Length > maxLength)
        {
            throw new ChatDomainException($"{fieldName} exceeds maximum length {maxLength}.");
        }
    }
}
