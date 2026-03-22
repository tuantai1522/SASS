namespace SASS.Chat.Domain.Exceptions;

public sealed class ChatDomainException : Exception
{
    public ChatDomainException(string message)
        : base(message)
    {
    }

    public ChatDomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
