using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace SASS.Chat.Features.Auth.Google.AuthorizeGoogle;

public sealed record AuthorizeGoogleQuery(string Code) : IRequest<AuthorizeGoogleResponse>;
