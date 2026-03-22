using SASS.Chat.Domain.Exceptions;

namespace SASS.Chat.Domain.AggregatesModel.Users;

public sealed class RefreshToken : Entity
{
    private RefreshToken()
    {
    }

    public static RefreshToken Create(Guid userId, string token, long expiredAt)
    {
        EnsureIdentity(userId, nameof(userId));
        EnsureRequiredText(token, nameof(token), 512);
        EnsureUnixMilliseconds(expiredAt, nameof(expiredAt));

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
        EnsureRequiredText(token, nameof(token), 512);
        EnsureUnixMilliseconds(expiredAt, nameof(expiredAt));

        Token = token;
        ExpiredAt = expiredAt;
    }

    private static void EnsureIdentity(Guid value, string fieldName)
    {
        if (value == Guid.Empty)
        {
            throw new ChatDomainException($"{fieldName} is invalid.");
        }
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

    private static void EnsureUnixMilliseconds(long value, string fieldName)
    {
        if (value <= 0)
        {
            throw new ChatDomainException($"{fieldName} must be unix milliseconds greater than zero.");
        }
    }
}
