using Microsoft.AspNetCore.Http;

namespace SASS.Chassis.WebStorages;

public sealed class CookieService(IHttpContextAccessor http) : ICookieService
{
    public void Set(string key, string value, long expiresAt)
    {
        var expiresAtOffset = DateTimeOffset.FromUnixTimeMilliseconds(expiresAt);

        http.HttpContext?.Response.Cookies.Append(key, value, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,                
            SameSite = SameSiteMode.None,
            Expires = expiresAtOffset,
            Path = "/"
        });
    }

    public string? Get(string key)
    {
        return http.HttpContext?.Request.Cookies[key];
    }

    public void Delete(string key)
    {
        http.HttpContext?.Response.Cookies.Delete(key, new CookieOptions
        {
            Path = "/",
            Secure = true,
            SameSite = SameSiteMode.None,
            HttpOnly = true
        });
    }
}