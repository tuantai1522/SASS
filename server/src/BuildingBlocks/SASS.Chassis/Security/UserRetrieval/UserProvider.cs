using System.Security.Authentication;
using Microsoft.AspNetCore.Http;
using SASS.Chassis.Security.Extensions;

namespace SASS.Chassis.Security.UserRetrieval;

internal sealed class UserProvider(IHttpContextAccessor httpContextAccessor) : IUserProvider
{
    public Guid UserId => 
        httpContextAccessor.HttpContext?.User.GetUserId() ??
        throw new AuthenticationException("You're not allowed to access this resource.");
}