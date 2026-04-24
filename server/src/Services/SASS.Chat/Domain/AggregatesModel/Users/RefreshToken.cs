namespace SASS.Chat.Domain.AggregatesModel.Users;

public sealed class RefreshToken : Entity
{
    private RefreshToken()
    {
    }

    public static RefreshToken Create(Guid userId, string token, long expiredAt)
    {
        return new RefreshToken
        {
            UserId = userId,
            Token = token,
            ExpiredAt = expiredAt
        };
    }

    public string Token { get; private set; } = null!;
    public long ExpiredAt { get; private set; }

    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;

    public void Rotate(string token, long expiredAt)
    {
        Token = token;
        ExpiredAt = expiredAt;
    }
}
