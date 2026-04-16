namespace SASS.Chat.Domain.AggregatesModel.Users;

public sealed class LocalCredential
{
    private LocalCredential()
    {
    }

    public static LocalCredential Create(Guid userId, string passwordHash, string passwordAlgo)
    {
        return new LocalCredential
        {
            UserId = userId,
            PasswordHash = passwordHash,
            PasswordAlgo = passwordAlgo,
        };
    }

    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;

    public string PasswordHash { get; private set; } = null!;
    public string PasswordAlgo { get; private set; } = null!;
    public long PasswordUpdatedAt { get; private set; } = DateTimeOffset.Now.ToUnixTimeSeconds();

    public void RotatePassword(string passwordHash, string passwordAlgo, long passwordUpdatedAt)
    {
        PasswordHash = passwordHash;
        PasswordAlgo = passwordAlgo;
        PasswordUpdatedAt = passwordUpdatedAt;
    }
}
