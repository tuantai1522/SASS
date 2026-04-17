namespace SASS.Chat.Features.Users.GetMe;

public sealed record GetMeResponse(Guid Id, string Email, string? AvatarUrl, string DisplayName);
