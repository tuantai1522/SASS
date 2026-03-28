namespace SASS.Chat.Features.Auth.Google.AuthorizeGoogle;

public sealed record AuthorizeGoogleResponse(Guid UserId, string Email, bool IsNewUser);

public sealed record AuthorizeGoogleErrorResponse(string Message);
